using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Enums;
using static MapAreaStruc;

public class ItemsStruc
{
    Form1 Form1_0;

    public string quality = "";
    public bool identified = false;
    public bool isSocketed = false;
    public bool inStore = false;
    public bool ethereal = false;
    public bool inpersonalstash = false;
    public uint txtFileNo = 0;
    public string itemCode = "";
    public int qualityNo = 0;
    public string ItemNAAME = "";
    public string localizedName = "";
    public string prefixName = "";
    public int itemLoc = 0;
    public ushort itemx = 0;
    public ushort itemy = 0;
    public int numSockets = 0;
    public int equiploc = 0;
    public byte[] itemdatastruc = new byte[144];
    public long ItemPointerLocation = 0;

    public long pUnitDataPtr = 0;
    public byte[] pUnitData = new byte[144];
    public uint flags = 0;

    public long pPathPtr = 0;
    public byte[] pPath = new byte[144];

    public uint statCount = 0;
    public uint statExCount = 0;
    public long statPtr = 0;
    public long statExPtr = 0;
    public byte[] statBuffer = new byte[] { };
    public byte[] statBufferEx = new byte[] { };
    public byte[] pStatB = new byte[180];
    public UInt32 itemQuality = 0;
    public long pStatsListExPtr = 0;

    public string LastPick = "";

    public int ItemsScanned = 0;
    public int ItemsOnGround = 0;
    public int ItemsInInventory = 0;
    public int ItemsEquiped = 0;
    public int ItemsInBelt = 0;

    public uint dwOwnerId = 0;
    public uint dwOwnerId_Shared1 = 0;
    public uint dwOwnerId_Shared2 = 0;
    public uint dwOwnerId_Shared3 = 0;
    public ushort invPage = 0;

    public bool ItemOnCursor = false;
    public string ItemOnCursorName = "";
    public bool UsePotionNotInRightSpot = true;

    public int TriesToPickItemCount = 0;
    public bool IsGrabbingItemOnGround = false;

    public List<long> BadItemsOnCursorIDList = new List<long>();
    public bool HasGotTheBadItemOnCursor = false;
    public Dictionary<string, bool> BadItemsOnGroundPointerList = new Dictionary<string, bool>();
    public Dictionary<string, bool> AvoidItemsOnGroundPointerList = new Dictionary<string, bool>();

    public bool AlreadyEmptyedInventory = false;

    public void SetForm1(Form1 Form1_1)
    {
        Form1_0 = Form1_1;
    }

    public void GetStatsAddr()
    {
        pStatsListExPtr = BitConverter.ToInt64(itemdatastruc, 0x88);

        /*pStatB = new byte[180];
        Form1_0.Mem_0.ReadRawMemory(pStatsListExPtr, ref pStatB, 180);
        statPtr = BitConverter.ToInt64(pStatB, 0x30);
        statCount = BitConverter.ToUInt32(pStatB, 0x38);
        statExPtr = BitConverter.ToInt64(pStatB, 0x88);
        statExCount = BitConverter.ToUInt32(pStatB, 0x90);*/

        statPtr = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(pStatsListExPtr + 0x30));
        statCount = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pStatsListExPtr + 0x38));
        statExPtr = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(pStatsListExPtr + 0x88));
        statExCount = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pStatsListExPtr + 0x90));

        //reset bad array size
        if (statCount > 50) statCount = 0;
        if (statExCount > 50) statExCount = 0;

        if (this.statCount > 0 && this.statCount < 100)
        {
            statBuffer = new byte[this.statCount * 10];
            Form1_0.Mem_0.ReadRawMemory(this.statPtr, ref statBuffer, (int)(this.statCount * 10));
        }
        else
        {
            statBuffer = new byte[] { };
        }
        if (this.statExCount > 0 && this.statExCount < 100)
        {
            statBufferEx = new byte[this.statExCount * 10];
            Form1_0.Mem_0.ReadRawMemory(this.statExPtr, ref statBufferEx, (int)(this.statExCount * 10));
        }
        else
        {
            statBufferEx = new byte[] { };
        }

        SetNumSockets();

        //string SavePathh = Form1_0.ThisEndPath + "DumpItempStatStruc";
        //File.Create(SavePathh).Dispose();
        //File.WriteAllBytes(SavePathh, statBuffer);
    }

    public void GetUnitData()
    {
        pUnitDataPtr = BitConverter.ToInt64(itemdatastruc, 0x10);

        pUnitData = new byte[0x56];
        Form1_0.Mem_0.ReadRawMemory(pUnitDataPtr, ref pUnitData, pUnitData.Length);
        /*itemQuality = BitConverter.ToUInt32(pUnitData, 0x00);
        setQuality((int)itemQuality);
        //uint SeedL = BitConverter.ToUInt32(pUnitData, 0x04);
        //uint SeedH = BitConverter.ToUInt32(pUnitData, 0x08);
        dwOwnerId = BitConverter.ToUInt32(pUnitData, 0x0c);
        flags = BitConverter.ToUInt32(pUnitData, 0x18);
        Form1_0.ItemsFlags_0.calculateFlags(flags);
        //uint uniqueOrSetId = BitConverter.ToUInt32(pUnitData, 0x34);
        equiploc = pUnitData[0x55];*/

        itemQuality = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pUnitDataPtr + 0x00));
        setQuality((int)itemQuality);
        //uint SeedL = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pUnitDataPtr + 0x04));
        //uint SeedH = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pUnitDataPtr + 0x08));
        dwOwnerId = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pUnitDataPtr + 0x0c));
        flags = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pUnitDataPtr + 0x18));
        Form1_0.ItemsFlags_0.calculateFlags(flags);
        //uint uniqueOrSetId = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pUnitDataPtr + 0x34));
        equiploc = Form1_0.Mem_0.ReadByteRaw((IntPtr)(pUnitDataPtr + 0x55));
        itemLoc = Form1_0.Mem_0.ReadByteRaw((IntPtr)(pUnitDataPtr + 0x54));

        //byte Thb = Form1_0.Mem_0.ReadByteRaw((IntPtr)(pUnitDataPtr + 0x04));
        //Console.WriteLine(ItemNAAME + ", class: " + Thb.ToString("X"));

        /*0 = INVPAGE_INVENTORY
          3 = INVPAGE_HORADRIC_CUBE
          4 = INVPAGE_STASH*/

        /*	emplacement si équipé at 0x55 - 1  <-----
        *	00 = noequip/inBelt
        *   01 = head
        *	02 = neck
        *	03 = tors
        *	04 = rarm
        *	05 = larm
        *	06 = lrin
        *	07 = rrin
        *	08 = belt
        *	09 = feet
        *	0A = glov
        *	0B = ralt
        *	0C = lalt
        */

        //string SavePathh = Form1_0.ThisEndPath + "DumpItempUnitDataStruc";
        //File.Create(SavePathh).Dispose();
        //File.WriteAllBytes(SavePathh, pUnitData);
    }

    public string GetAllFlagsFromItem()
    {
        string AllFlags = "";

        if (Form1_0.ItemsStruc_0.identified)
        {
            if (AllFlags != "") AllFlags += " && ";
            AllFlags += "[Flag] == identified";
        }
        else
        {
            if (AllFlags != "") AllFlags += " && ";
            AllFlags += "[Flag] == unidentified";
        }
        if (Form1_0.ItemsStruc_0.isSocketed)
        {
            if (AllFlags != "") AllFlags += " && ";
            AllFlags += "[Flag] == socketed";
        }
        if (Form1_0.ItemsStruc_0.ethereal)
        {
            if (AllFlags != "") AllFlags += " && ";
            AllFlags += "[Flag] == ethereal";
        }

        return AllFlags;
    }

    public void GetUnitPathData()
    {
        pPathPtr = BitConverter.ToInt64(itemdatastruc, 0x38);
        //pPath = new byte[0x16];
        //Form1_0.Mem_0.ReadRawMemory(pPathPtr, ref pPath, pPath.Length);
        //itemx = BitConverter.ToUInt16(pPath, 0x10);
        //itemy = BitConverter.ToUInt16(pPath, 0x14);
        itemx = Form1_0.Mem_0.ReadUInt16Raw((IntPtr)(pPathPtr + 0x10));
        itemy = Form1_0.Mem_0.ReadUInt16Raw((IntPtr)(pPathPtr + 0x14));

        /*string SavePathh = Form1_0.ThisEndPath + "DumpItempPathStruc";
        File.Create(SavePathh).Dispose();
        File.WriteAllBytes(SavePathh, pPath);*/
    }

    public void GetItemAtPointer(long AtPointerr)
    {
        ItemPointerLocation = AtPointerr;
        if (ItemPointerLocation > 0)
        {
            itemdatastruc = new byte[144];
            Form1_0.Mem_0.ReadRawMemory(ItemPointerLocation, ref itemdatastruc, 144);

            ItemNAAME = Form1_0.ItemsNames_0.getItemBaseName(BitConverter.ToUInt32(itemdatastruc, 4));
            txtFileNo = BitConverter.ToUInt32(itemdatastruc, 4);
            GetUnitData();
            GetUnitPathData();
            GetStatsAddr();
        }
    }

    public bool GetShopItem(string ShopItemName)
    {
        try
        {
            Form1_0.PatternsScan_0.scanForUnitsPointer("item");
            foreach (var ThisCurrentPointer in Form1_0.PatternsScan_0.AllItemsPointers)
            {
                ItemPointerLocation = ThisCurrentPointer.Key;
                if (ItemPointerLocation > 0)
                {
                    itemdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(ItemPointerLocation, ref itemdatastruc, 144);

                    ItemsScanned++;
                    ItemNAAME = Form1_0.ItemsNames_0.getItemBaseName(BitConverter.ToUInt32(itemdatastruc, 4));
                    txtFileNo = BitConverter.ToUInt32(itemdatastruc, 4);
                    GetUnitData();
                    GetUnitPathData();
                    GetStatsAddr();

                    //Form1_0.method_1("ItemType: " + BitConverter.ToUInt32(itemdatastruc, 0).ToString() + ", TxtFileNo: " + BitConverter.ToUInt32(itemdatastruc, 4).ToString() + ", Name: " + ItemNAAME + ", Location: " + GetItemLocation(itemdatastruc[0x0C]));
                    //; itemLoc - 0 in inventory, 1 equipped, 2 in belt, 3 on ground, 4 cursor, 5 dropping, 6 socketed
                    if (itemdatastruc[0x0C] == 0)
                    {
                        if (dwOwnerId != Form1_0.PlayerScan_0.unitId)
                        {
                            if (equiploc <= 3)
                            {
                                if (ItemNAAME == ShopItemName)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            Form1_0.method_1("Couldn't 'GetShopItem()'", Color.OrangeRed);
        }

        return false;
    }

    public void DebugItems()
    {
        Form1_0.ClearDebugItems();

        AllItemsOnCursor = new List<string>();
        AllItemsInInventory = new List<string>();
        AllItemsInStash = new List<string>();
        AllItemsInSharedStash1 = new List<string>();
        AllItemsInSharedStash2 = new List<string>();
        AllItemsInSharedStash3 = new List<string>();
        AllItemsIncube = new List<string>();
        AllItemsInShop = new List<string>();
        AllItemsEquipped = new List<string>();
        AllItemsInBelt = new List<string>();
        AllItemsOnGround = new List<string>();
        AllItemsOthers = new List<string>();

        DebuggingItems = true;
        GetItems(false);
        DebuggingItems = false;

        string CurrentAllItemsText = "";
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 1) for (int i = 0; i < AllItemsOnCursor.Count; i++) CurrentAllItemsText += AllItemsOnCursor[i] + Environment.NewLine;
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 2) for (int i = 0; i < AllItemsInInventory.Count; i++) CurrentAllItemsText += AllItemsInInventory[i] + Environment.NewLine;
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 3) for (int i = 0; i < AllItemsInStash.Count; i++) CurrentAllItemsText += AllItemsInStash[i] + Environment.NewLine;
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 4) for (int i = 0; i < AllItemsInSharedStash1.Count; i++) CurrentAllItemsText += AllItemsInSharedStash1[i] + Environment.NewLine;
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 5) for (int i = 0; i < AllItemsInSharedStash2.Count; i++) CurrentAllItemsText += AllItemsInSharedStash2[i] + Environment.NewLine;
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 6) for (int i = 0; i < AllItemsInSharedStash3.Count; i++) CurrentAllItemsText += AllItemsInSharedStash3[i] + Environment.NewLine;
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 7) for (int i = 0; i < AllItemsIncube.Count; i++) CurrentAllItemsText += AllItemsIncube[i] + Environment.NewLine;
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 8) for (int i = 0; i < AllItemsEquipped.Count; i++) CurrentAllItemsText += AllItemsEquipped[i] + Environment.NewLine;
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 9) for (int i = 0; i < AllItemsInBelt.Count; i++) CurrentAllItemsText += AllItemsInBelt[i] + Environment.NewLine;
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 10) for (int i = 0; i < AllItemsOnGround.Count; i++) CurrentAllItemsText += AllItemsOnGround[i] + Environment.NewLine;
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 11) for (int i = 0; i < AllItemsInShop.Count; i++) CurrentAllItemsText += AllItemsInShop[i] + Environment.NewLine;
        if (Form1_0.comboBoxItemsCategory.SelectedIndex == 0 || Form1_0.comboBoxItemsCategory.SelectedIndex == 12) for (int i = 0; i < AllItemsOthers.Count; i++) CurrentAllItemsText += AllItemsOthers[i] + Environment.NewLine;

        if (CurrentAllItemsText != AllItemsText && CurrentAllItemsText != "")
        {
            AllItemsText = CurrentAllItemsText;
            Form1_0.AppendTextDebugItems(AllItemsText);
        }
    }

    public string GetItemsStashInfosTxt()
    {
        string ThisInfos = ", StashItem:";
        if (Form1_0.ItemsAlert_0.ShouldKeepItem()) ThisInfos += "true";
        else ThisInfos += "false";

        ThisInfos += ", ItemToID:";
        if (Form1_0.ItemsAlert_0.ShouldPickItem(false)) ThisInfos += "true";
        else ThisInfos += "false";

        //Console.WriteLine(Form1_0.ItemsStruc_0.ItemNAAME + ", StashItem:" + InventoryHasStashItem[FullIndex] + ", ItemToID:" + InventoryHasItemToID[FullIndex] + ", UnidItem:" + InventoryHasUnidItem[FullIndex]);
        return ThisInfos;
    }

    public bool DebuggingItems = false;

    public List<string> AllItemsOnCursor = new List<string>();
    public List<string> AllItemsInInventory = new List<string>();
    public List<string> AllItemsInStash = new List<string>();
    public List<string> AllItemsInSharedStash1 = new List<string>();
    public List<string> AllItemsInSharedStash2 = new List<string>();
    public List<string> AllItemsInSharedStash3 = new List<string>();
    public List<string> AllItemsIncube = new List<string>();
    public List<string> AllItemsInShop = new List<string>();
    public List<string> AllItemsEquipped = new List<string>();
    public List<string> AllItemsInBelt = new List<string>();
    public List<string> AllItemsOnGround = new List<string>();
    public List<string> AllItemsOthers = new List<string>();

    public string AllItemsText = "";

    public void GetBadItemsOnCursor()
    {
        HasGotTheBadItemOnCursor = false;
        GetItems(false);
        HasGotTheBadItemOnCursor = true;
    }

    public bool GetItems(bool IsPickingItem)
    {
        try
        {
            if (!Form1_0.GameStruc_0.IsInGame()) return false;

            //dead leave game
            if (Form1_0.PlayerScan_0.PlayerDead || Form1_0.Potions_0.ForceLeave)
            {
                Form1_0.Potions_0.ForceLeave = true;
                Form1_0.BaalLeech_0.SearchSameGamesAsLastOne = false;
                Form1_0.LeaveGame(false);
                Form1_0.IncreaseDeadCount();
                return false;
            }

            //Form1_0.SetGameStatus("SCANING ITEMS");
            ItemsScanned = 0;
            ItemsOnGround = 0;
            ItemsEquiped = 0;
            ItemsInInventory = 0;
            ItemsInBelt = 0;
            if (!IsPickingItem)
            {
                Form1_0.Repair_0.ShouldRepair = false;
                Form1_0.BeltStruc_0.BeltHaveItems = new int[16];
                Form1_0.BeltStruc_0.BeltItemsTypes = new int[16];
                Form1_0.BeltStruc_0.HPQuantity = 0;
                Form1_0.BeltStruc_0.ManyQuantity = 0;
                Form1_0.InventoryStruc_0.ResetInventory();
                Form1_0.PlayerScan_0.HPFromEquippedItems = 0;
                Form1_0.PlayerScan_0.ManaFromEquippedItems = 0;
                Form1_0.PlayerScan_0.VitalityFromEquippedItems = 0;
                Form1_0.PlayerScan_0.EnergyFromEquippedItems = 0;
                Form1_0.PlayerScan_0.HPPercentFromEquippedItems = 0;
                Form1_0.PlayerScan_0.ManaPercentFromEquippedItems = 0;
                Form1_0.StashStruc_0.ResetStashInventory();
                Form1_0.Cubing_0.ResetCubeInventory();
                Form1_0.InventoryStruc_0.HasIDTome = false;
                ItemOnCursor = false;
                ItemOnCursorName = "";
            }
            else
            {
                if (TriesToPickItemCount >= CharConfig.MaxItemGrabTries) return false;
            }

            Form1_0.PatternsScan_0.scanForUnitsPointer("item");

            foreach (var ThisCurrentPointer in Form1_0.PatternsScan_0.AllItemsPointers)
            {
                ItemPointerLocation = ThisCurrentPointer.Key;
                if (ItemPointerLocation > 0)
                {
                    itemdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(ItemPointerLocation, ref itemdatastruc, 144);

                    ItemsScanned++;
                    txtFileNo = BitConverter.ToUInt32(itemdatastruc, 4);
                    uint ItemID = BitConverter.ToUInt32(itemdatastruc, 8);
                    ItemNAAME = Form1_0.ItemsNames_0.getItemBaseName(txtFileNo);
                    GetUnitData();
                    GetUnitPathData();
                    GetStatsAddr();

                    //Form1_0.method_1("ItemType: " + BitConverter.ToUInt32(itemdatastruc, 0).ToString() + ", TxtFileNo: " + BitConverter.ToUInt32(itemdatastruc, 4).ToString() + ", Name: " + ItemNAAME + ", Location: " + GetItemLocation(itemdatastruc[0x0C]));
                    //; itemLoc - 0 in inventory, 1 equipped, 2 in belt, 3 on ground, 4 cursor, 5 dropping, 6 socketed

                    if (itemdatastruc[0x0C] == 4)
                    {
                        if (!IsPickingItem && !HasGotTheBadItemOnCursor && !IsIncludedInList(BadItemsOnCursorIDList, ItemPointerLocation))
                        {
                            if (ItemNAAME != "Horadric Cube")
                            {
                                Form1_0.method_1("Added bad item 'OnCursor':" + ItemNAAME, Color.OrangeRed);
                                BadItemsOnCursorIDList.Add(ItemPointerLocation);
                            }
                        }
                        else if (HasGotTheBadItemOnCursor && !IsIncludedInList(BadItemsOnCursorIDList, ItemPointerLocation))
                        {
                            if (ItemNAAME == "Short Sword")
                            {
                                Form1_0.method_1("Added bad item 'OnCursor':" + ItemNAAME, Color.OrangeRed);
                                BadItemsOnCursorIDList.Add(ItemPointerLocation);
                                continue;
                            }

                            ItemOnCursor = true;
                            ItemOnCursorName = ItemNAAME;
                            //Form1_0.method_1("cursor: " + ItemNAAME + " - at: " + itemx + "," + itemy, Color.BlueViolet);

                            if (DebuggingItems)
                            {
                                AllItemsOnCursor.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - On Cursor - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                            }
                        }
                    }

                    if (itemdatastruc[0x0C] == 0)
                    {
                        if (dwOwnerId == Form1_0.PlayerScan_0.unitId && equiploc == 0)
                        {
                            ItemsInInventory++;

                            //Form1_0.method_1("inv: " + ItemNAAME + " - at: " + itemx + "," + itemy, Color.Red);
                            if (!IsPickingItem)
                            {
                                Form1_0.PlayerScan_0.GetHPAndManaOnThisEquippedItem();
                                Form1_0.InventoryStruc_0.SetInventoryItem();
                                Form1_0.InventoryStruc_0.SetHUDItem();
                            }

                            if (DebuggingItems)
                            {
                                AllItemsInInventory.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - In Inventory - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                            }
                        }

                        else if (dwOwnerId == Form1_0.PlayerScan_0.unitId && equiploc == 4)
                        {
                            //here for items in stash
                            //Form1_0.method_1("name: " + ItemNAAME + " - at: " + itemx + "," + itemy, Color.DarkGreen);
                            Form1_0.StashStruc_0.AddStashItem(itemx, itemy, 1);

                            if (DebuggingItems)
                            {
                                AllItemsInStash.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - In Stash - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                            }
                        }
                        else if (dwOwnerId != Form1_0.PlayerScan_0.unitId && equiploc == 4)
                        {
                            //here for items in shared stash
                            //Form1_0.method_1("name: " + ItemNAAME + " - at: " + itemx + "," + itemy + " - " + dwOwnerId, Color.DarkGreen);
                            SetSharedStashOwner();
                            if (dwOwnerId_Shared1 != 0 && dwOwnerId_Shared2 != 0 && dwOwnerId_Shared3 != 0)
                            {
                                int StashNum = 0;
                                if (dwOwnerId == dwOwnerId_Shared1) StashNum = 2;
                                if (dwOwnerId == dwOwnerId_Shared2) StashNum = 3;
                                if (dwOwnerId == dwOwnerId_Shared3) StashNum = 4;
                                Form1_0.StashStruc_0.AddStashItem(itemx, itemy, StashNum);

                                if (dwOwnerId == dwOwnerId_Shared1 && DebuggingItems) AllItemsInSharedStash1.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - In Shared Stash1 - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                                if (dwOwnerId == dwOwnerId_Shared2 && DebuggingItems) AllItemsInSharedStash2.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - In Shared Stash2 - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                                if (dwOwnerId == dwOwnerId_Shared3 && DebuggingItems) AllItemsInSharedStash3.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - In Shared Stash3 - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                            }
                        }
                        else if (dwOwnerId == Form1_0.PlayerScan_0.unitId && equiploc == 3)
                        {
                            //here for items in cube
                            //Form1_0.method_1("name: " + ItemNAAME + " - at: " + itemx + "," + itemy, Color.DarkGreen);
                            Form1_0.Cubing_0.AddCubeItem(itemx, itemy);

                            if (DebuggingItems)
                            {
                                AllItemsIncube.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - In Cube - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                            }
                        }
                        else if (dwOwnerId != Form1_0.PlayerScan_0.unitId && equiploc <= 3)
                        {
                            //Shop Items
                            if (DebuggingItems)
                            {
                                AllItemsInShop.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - In Shop - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                            }
                        }
                        /*else if (dwOwnerId != Form1_0.PlayerScan_0.unitId && equiploc == 0)
                        {
                            //Shop Gamble Items
                            if (DebuggingItems)
                            {
                                AllItemsInShop.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - In Shop(Gamble) - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                            }
                        }*/
                        else
                        {
                            AllItemsOthers.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - Others - EquipLocation:" + equiploc + " - SelfOwner:" + (dwOwnerId == Form1_0.PlayerScan_0.unitId) + " - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                        }
                    }
                    if (itemdatastruc[0x0C] == 1)
                    {
                        if (dwOwnerId == Form1_0.PlayerScan_0.unitId && equiploc == 255)
                        {
                            //Form1_0.method_1("inv: " + ItemNAAME + " - at: " + itemx + "," + itemy, Color.Red);

                            ItemsEquiped++;
                            if (!IsPickingItem)
                            {
                                Form1_0.PlayerScan_0.GetHPAndManaOnThisEquippedItem();
                                Form1_0.Repair_0.GetDurabilityOnThisEquippedItem();
                            }
                            //Form1_0.method_1("name: " + ItemNAAME + " - at: " + itemx + "," + itemy + " - " + equiploc, Color.DarkGreen);

                            if (DebuggingItems)
                            {
                                AllItemsEquipped.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - Equipped - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                            }

                            /*if (ItemNAAME == "Crusader Gauntlets")
                            {
                                string SavePathh = Form1_0.ThisEndPath + "DumpItempUnitDataStruc";
                                File.Create(SavePathh).Dispose();
                                File.WriteAllBytes(SavePathh, pUnitData);
                            }*/
                        }
                        else
                        {
                            /*if (dwOwnerId != 0 && Form1_0.MercStruc_0.MercOwnerID == 0)
                            {
                                //Form1_0.MercStruc_0.MercOwnerID = ItemID;
                                Form1_0.method_1("owner: " + dwOwnerId.ToString("X") + ", ID: " + ItemID.ToString("X") + ", name: " + ItemNAAME + " - at: " + itemx + "," + itemy + " - " + equiploc, Color.DarkGreen);
                            }*/
                        }
                    }
                    if (itemdatastruc[0x0C] == 2)
                    {
                        if (dwOwnerId == Form1_0.PlayerScan_0.unitId)
                        {
                            ItemsInBelt++;
                            if (!IsPickingItem)
                            {
                                Form1_0.BeltStruc_0.AddBeltItem(UsePotionNotInRightSpot);
                            }

                            if (DebuggingItems)
                            {
                                AllItemsInBelt.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - In Belt - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                            }
                        }
                    }
                    //; on ground, dropping
                    if (itemdatastruc[0x0C] == 3 || itemdatastruc[0x0C] == 5)
                    {
                        ItemsOnGround++;

                        //Form1_0.method_1_Items("Ground: " + ItemNAAME, GetColorFromQuality((int)itemQuality));
                        if (DebuggingItems)
                        {
                            AllItemsOnGround.Add("ID:" + txtFileNo + "(" + ItemNAAME + ") at:" + itemx + ", " + itemy + " - On Ground/Droping - " + Form1_0.ItemsAlert_0.GetItemTypeText() + " && " + GetQualityTextString() + " && " + GetAllFlagsFromItem() + " && " + GetAllValuesFromStats() + GetItemsStashInfosTxt());
                        }

                        if (!IsPickingItem) continue;
                        if (BadItemsOnGroundPointerList.ContainsKey(ItemPointerLocation.ToString())) continue;
                        if (AvoidItemsOnGroundPointerList.ContainsKey(ItemPointerLocation.ToString()))
                        {
                            if (!IsItemPickingPotion())
                            {
                                continue;
                            }
                        }

                        Form1_0.UIScan_0.readUI();
                        if (Form1_0.UIScan_0.leftMenu || Form1_0.UIScan_0.rightMenu || Form1_0.UIScan_0.fullMenu) continue;

                        if ((Form1_0.ItemsAlert_0.ShouldPickItem(false) || Form1_0.BeltStruc_0.ItemGrabPotion()))
                        {
                            /*string SavePathh = Form1_0.ThisEndPath + "DumpItempPathStruc";
                            File.Create(SavePathh).Dispose();
                            File.WriteAllBytes(SavePathh, itemdatastruc);*/

                            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, itemx, itemy);
                            if (ShouldPickPos(itemScreenPos))
                            {
                                int DiffXPlayer = itemx - Form1_0.PlayerScan_0.xPosFinal;
                                int DiffYPlayer = itemy - Form1_0.PlayerScan_0.yPosFinal;
                                if (DiffXPlayer < 0) DiffXPlayer = -DiffXPlayer;
                                if (DiffYPlayer < 0) DiffYPlayer = -DiffYPlayer;

                                if (DiffXPlayer > 100 || DiffYPlayer > 100)
                                {
                                    continue;
                                }

                                //####
                                if (CharConfig.UseTeleport)
                                {
                                    if (DiffXPlayer > 4 || DiffYPlayer > 4)
                                    {
                                        //Form1_0.Mover_0.MoveToLocation(itemx, itemy); //slow move
                                        Form1_0.Mover_0.MoveToLocationAttack(itemx, itemy); //fast move
                                        Form1_0.PlayerScan_0.GetPositions();
                                        GetUnitPathData();
                                        itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, itemx, itemy);
                                    }
                                }

                                //##############################################
                                //##############################################
                                //detect bad items??
                                if (((itemx <= 0 && itemy <= 0)
                                    || (itemScreenPos.X <= 0 && itemScreenPos.Y <= 0)))
                                {
                                    if (!BadItemsOnGroundPointerList.ContainsKey(ItemPointerLocation.ToString()))
                                    {
                                        Form1_0.method_1("Added bad item 'OnGround':" + ItemNAAME, Color.OrangeRed);
                                        BadItemsOnGroundPointerList.Add(ItemPointerLocation.ToString(), true);
                                        continue;
                                    }
                                }
                                if (BadItemsOnGroundPointerList.ContainsKey(ItemPointerLocation.ToString()))
                                {
                                    //Form1_0.method_1("Avoided bad item 'OnGround':" + ItemNAAME, Color.OrangeRed);
                                    continue;
                                }
                                //##############################################
                                //##############################################

                                //####
                                TriesToPickItemCount++;
                                Form1_0.KeyMouse_0.PressKeyHold(CharConfig.KeyForceMovement);
                                //Form1_0.KeyMouse_0.MouseMoveTo_RealPos(itemScreenPos.X, itemScreenPos.Y);
                                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
                                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y); //clic twice??
                                Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);

                                if (ItemNAAME != LastPick)
                                {
                                    LastPick = ItemNAAME;
                                    Form1_0.method_1_Items("Picked: " + ItemNAAME, GetColorFromQuality((int)itemQuality));

                                    //##############################################
                                    //##############################################
                                    //detect bad items??
                                    if (((itemx <= 0 && itemy <= 0)
                                        || (itemScreenPos.X <= 0 && itemScreenPos.Y <= 0)))
                                    {
                                        if (!BadItemsOnGroundPointerList.ContainsKey(ItemPointerLocation.ToString()))
                                        {
                                            Form1_0.method_1("Added bad item 'OnGround':" + ItemNAAME, Color.OrangeRed);
                                            BadItemsOnGroundPointerList.Add(ItemPointerLocation.ToString(), true);
                                            continue;
                                        }
                                    }
                                    //##############################################
                                    //##############################################

                                    Form1_0.BeltStruc_0.CheckForMissingPotions();
                                    /*Form1_0.BeltStruc_0.ItemIsPotion();
                                    if (Form1_0.BeltStruc_0.IsItemHPPotion
                                        || Form1_0.BeltStruc_0.IsItemManaPotion
                                        || Form1_0.BeltStruc_0.IsItemRVPotion
                                        || Form1_0.BeltStruc_0.IsItemFullRVPotion)
                                    {
                                        Form1_0.BeltStruc_0.CheckForMissingPotions();
                                    }*/
                                }

                                //after a lot of try picking item, inventory might be full, dump bad item to ground
                                if (TriesToPickItemCount >= 7)
                                {
                                    Form1_0.InventoryStruc_0.DumpBadItemsOnGround();
                                }

                                //after a lot of try picking item, inventory might be full, go to town
                                if (TriesToPickItemCount >= CharConfig.MaxItemGrabTries)
                                {
                                    Form1_0.InventoryStruc_0.DumpBadItemsOnGround();
                                    TriesToPickItemCount = CharConfig.MaxItemGrabTries;
                                    Form1_0.Town_0.GoToTown();
                                    IsGrabbingItemOnGround = false;
                                    return false;
                                }
                                IsGrabbingItemOnGround = true;
                                return true;
                            }
                        }
                        else
                        {
                            if (!IsItemPickingPotion() && !AvoidItemsOnGroundPointerList.ContainsKey(ItemPointerLocation.ToString()))
                            {
                                //Form1_0.method_1("Added avoid item 'OnGround':" + ItemNAAME, Color.OrangeRed);
                                AvoidItemsOnGroundPointerList.Add(ItemPointerLocation.ToString(), true);
                            }
                        }
                    }
                }
            }

            if (IsPickingItem) TriesToPickItemCount = 0; //nothing to pick!
            if (IsPickingItem) IsGrabbingItemOnGround = false;
            if (IsPickingItem) AlreadyEmptyedInventory = false;
            //Form1_0.method_1("-----", Color.Black);

            return false;
        }
        catch
        {
            Form1_0.method_1("Couldn't 'GetItems()'", Color.OrangeRed);
        }
        return false;
    }

    public bool GetSpecificItem(int ItemLocation, string ItemName, int PositionX, int PositionY, uint dwOwnerId, int Thisequiploc, bool InventoryItem = true)
    {
        try
        {
            if (!Form1_0.GameStruc_0.IsInGame()) return false;

            //dead leave game
            if (Form1_0.PlayerScan_0.PlayerDead || Form1_0.Potions_0.ForceLeave)
            {
                Form1_0.Potions_0.ForceLeave = true;
                Form1_0.BaalLeech_0.SearchSameGamesAsLastOne = false;
                Form1_0.LeaveGame(false);
                Form1_0.IncreaseDeadCount();
                return false;
            }

            Form1_0.PatternsScan_0.scanForUnitsPointer("item");
            foreach (var ThisCurrentPointer in Form1_0.PatternsScan_0.AllItemsPointers)
            {
                ItemPointerLocation = ThisCurrentPointer.Key;
                if (ItemPointerLocation > 0)
                {
                    itemdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(ItemPointerLocation, ref itemdatastruc, 144);

                    ItemsScanned++;
                    txtFileNo = BitConverter.ToUInt32(itemdatastruc, 4);
                    uint ItemID = BitConverter.ToUInt32(itemdatastruc, 8);
                    ItemNAAME = Form1_0.ItemsNames_0.getItemBaseName(txtFileNo);
                    GetUnitData();
                    GetUnitPathData();
                    GetStatsAddr();

                    //Form1_0.method_1("ItemType: " + BitConverter.ToUInt32(itemdatastruc, 0).ToString() + ", TxtFileNo: " + BitConverter.ToUInt32(itemdatastruc, 4).ToString() + ", Name: " + ItemNAAME + ", Location: " + GetItemLocation(itemdatastruc[0x0C]));
                    //; itemLoc - 0 in inventory, 1 equipped, 2 in belt, 3 on ground, 4 cursor, 5 dropping, 6 socketed
                    if (itemdatastruc[0x0C] == ItemLocation)
                    {
                        bool CanGo = true;
                        if (itemdatastruc[0x0C] == 0)
                        {
                            if (dwOwnerId != 0 && dwOwnerId == Form1_0.PlayerScan_0.unitId && Thisequiploc == equiploc)
                            {
                                if (!InventoryItem) CanGo = false;
                            }
                        }

                        if (InventoryItem && dwOwnerId != Form1_0.PlayerScan_0.unitId) CanGo = false;
                        if (Thisequiploc != equiploc) CanGo = false;

                        if (((ItemName != "" && ItemNAAME == ItemName) || ItemName == "") && CanGo)
                        {
                            if (itemx == PositionX && itemy == PositionY)
                            {
                                //Console.WriteLine(Form1_0.ItemsStruc_0.ItemNAAME);
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
        catch
        {
            Form1_0.method_1("Couldn't 'GetSpecificItem()'", Color.OrangeRed);
        }
        return false;
    }

    public bool ShopBotGetPurchaseItems()
    {
        try
        {
            if (!Form1_0.GameStruc_0.IsInGame()) return false;

            //dead leave game
            if (Form1_0.PlayerScan_0.PlayerDead || Form1_0.Potions_0.ForceLeave)
            {
                Form1_0.Potions_0.ForceLeave = true;
                Form1_0.BaalLeech_0.SearchSameGamesAsLastOne = false;
                Form1_0.LeaveGame(false);
                Form1_0.IncreaseDeadCount();
                return false;
            }

            Form1_0.PatternsScan_0.scanForUnitsPointer("item");
            foreach (var ThisCurrentPointer in Form1_0.PatternsScan_0.AllItemsPointers)
            {
                ItemPointerLocation = ThisCurrentPointer.Key;
                if (ItemPointerLocation > 0)
                {
                    itemdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(ItemPointerLocation, ref itemdatastruc, 144);

                    ItemsScanned++;
                    txtFileNo = BitConverter.ToUInt32(itemdatastruc, 4);
                    uint ItemID = BitConverter.ToUInt32(itemdatastruc, 8);
                    ItemNAAME = Form1_0.ItemsNames_0.getItemBaseName(txtFileNo);
                    GetUnitData();
                    GetUnitPathData();
                    GetStatsAddr();

                    //Form1_0.method_1("ItemType: " + BitConverter.ToUInt32(itemdatastruc, 0).ToString() + ", TxtFileNo: " + BitConverter.ToUInt32(itemdatastruc, 4).ToString() + ", Name: " + ItemNAAME + ", Location: " + GetItemLocation(itemdatastruc[0x0C]));
                    //; itemLoc - 0 in inventory, 1 equipped, 2 in belt, 3 on ground, 4 cursor, 5 dropping, 6 socketed
                    if (itemdatastruc[0x0C] == 0)
                    {
                        if (dwOwnerId != Form1_0.PlayerScan_0.unitId && equiploc <= 3)
                        {
                            //if (Form1_0.ItemsAlert_0.ShouldPickItem(true))
                            if (Form1_0.ItemsAlert_0.ShouldPickItem(false))
                            {
                                if (equiploc == 0) Form1_0.KeyMouse_0.MouseClicc(220, 200);   //clic shop1
                                if (equiploc == 1) Form1_0.KeyMouse_0.MouseClicc(350, 200);   //clic shop2
                                if (equiploc == 2) Form1_0.KeyMouse_0.MouseClicc(475, 200);   //clic shop3
                                if (equiploc == 3) Form1_0.KeyMouse_0.MouseClicc(600, 200);   //clic shop4
                                Form1_0.WaitDelay(15);

                                Dictionary<string, int> itemScreenPos = Form1_0.Shop_0.ConvertShopLocToScreenPos(Form1_0.ItemsStruc_0.itemx, Form1_0.ItemsStruc_0.itemy);
                                Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                                Form1_0.WaitDelay(15);

                                Form1_0.Town_0.Towning = true;
                            }
                        }
                    }
                }
            }

            return false;
        }
        catch
        {
            Form1_0.method_1("Couldn't 'ShopBotGetPurchaseItems()'", Color.OrangeRed);
        }
        return false;
    }

    public bool IsItemPickingPotion()
    {
        bool IsItemPickingPotion = false;

        string ThisItemName = Form1_0.ItemsStruc_0.ItemNAAME.Replace(" ", "");
        foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsPotions)
        {
            if (ThisItemName == Regex.Replace(ThisDir.Key.Replace(" ", ""), @"[\d-]", string.Empty) && ThisDir.Value)
            {
                if (ThisItemName.Contains("Healing")) IsItemPickingPotion = true;
                if (ThisItemName.Contains("Mana")) IsItemPickingPotion = true;
                if (ThisItemName.Contains("Rejuvenation")) IsItemPickingPotion = true;
                if (ThisItemName.Contains("FullRejuvenation")) IsItemPickingPotion = true;

                if (IsItemPickingPotion) break;
            }
        }

        return IsItemPickingPotion;
    }

    public bool IsIncludedInList(List<long> IgnoredIDList, long ThisID)
    {
        if (IgnoredIDList != null)
        {
            if (IgnoredIDList.Count > 0)
            {
                for (int i = 0; i < IgnoredIDList.Count; i++)
                {
                    if (IgnoredIDList[i] == ThisID)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool PickThisItem(string ThisItemName)
    {
        try
        {
            if (!Form1_0.GameStruc_0.IsInGame()) return false;

            //dead leave game
            if (Form1_0.PlayerScan_0.PlayerDead || Form1_0.Potions_0.ForceLeave)
            {
                Form1_0.Potions_0.ForceLeave = true;
                Form1_0.BaalLeech_0.SearchSameGamesAsLastOne = false;
                Form1_0.LeaveGame(false);
                Form1_0.IncreaseDeadCount();
                return false;
            }

            Form1_0.PlayerScan_0.GetPositions();

            Form1_0.PatternsScan_0.scanForUnitsPointer("item");
            foreach (var ThisCurrentPointer in Form1_0.PatternsScan_0.AllItemsPointers)
            {
                ItemPointerLocation = ThisCurrentPointer.Key;
                if (ItemPointerLocation > 0)
                {
                    itemdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(ItemPointerLocation, ref itemdatastruc, 144);

                    ItemNAAME = Form1_0.ItemsNames_0.getItemBaseName(BitConverter.ToUInt32(itemdatastruc, 4));
                    txtFileNo = BitConverter.ToUInt32(itemdatastruc, 4);
                    GetUnitData();
                    GetUnitPathData();
                    GetStatsAddr();

                    //; on ground, dropping
                    if (itemdatastruc[0x0C] == 3 || itemdatastruc[0x0C] == 5)
                    {
                        Form1_0.UIScan_0.readUI();
                        if (ItemNAAME == ThisItemName && (!Form1_0.UIScan_0.leftMenu && !Form1_0.UIScan_0.rightMenu && !Form1_0.UIScan_0.fullMenu))
                        {
                            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, itemx, itemy);
                            if (ShouldPickPos(itemScreenPos))
                            {
                                int DiffXPlayer = itemx - Form1_0.PlayerScan_0.xPosFinal;
                                int DiffYPlayer = itemy - Form1_0.PlayerScan_0.yPosFinal;
                                if (DiffXPlayer < 0) DiffXPlayer = -DiffXPlayer;
                                if (DiffYPlayer < 0) DiffYPlayer = -DiffYPlayer;

                                if (DiffXPlayer > 100 || DiffYPlayer > 100)
                                {
                                    continue;
                                }

                                //####
                                if (CharConfig.UseTeleport)
                                {

                                    if (DiffXPlayer > 4 || DiffYPlayer > 4)
                                    {
                                        Form1_0.Mover_0.MoveToLocation(itemx, itemy);
                                        Form1_0.PlayerScan_0.GetPositions();
                                        GetUnitPathData();
                                        itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, itemx, itemy);
                                    }
                                }
                                //####

                                TriesToPickItemCount++;
                                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
                                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);

                                if (ItemNAAME != LastPick)
                                {
                                    LastPick = ItemNAAME;
                                    Form1_0.method_1_Items("Picked: " + ItemNAAME, GetColorFromQuality((int)itemQuality));
                                }

                                //after a lot of try picking item, inventory might be full, go to town
                                if (TriesToPickItemCount >= CharConfig.MaxItemGrabTries)
                                {
                                    TriesToPickItemCount = 0;
                                    Form1_0.Town_0.GoToTown();
                                    return false;
                                }
                                return true;
                            }
                        }
                        else
                        {
                            TriesToPickItemCount = 0; //nothing to pick!
                        }
                    }
                }
            }
        }
        catch
        {
            Form1_0.method_1("Couldn't 'PickThisItem()'", Color.OrangeRed);
        }

        return false;
    }

    public void SetSharedStashOwner()
    {
        if (ItemNAAME == CharConfig.DummyItemSharedStash1)
        {
            dwOwnerId_Shared1 = dwOwnerId;
        }
        if (ItemNAAME == CharConfig.DummyItemSharedStash2)
        {
            dwOwnerId_Shared2 = dwOwnerId;
        }
        if (ItemNAAME == CharConfig.DummyItemSharedStash3)
        {
            dwOwnerId_Shared3 = dwOwnerId;
        }
    }

    public void GrabAllItemsForGold()
    {
        if (!CharConfig.GrabForGold) return;

        string LastGrabbedItem = "";
        int TryGrabCount = 0;
        int ItemsGrabbed = 0;

        if (Form1_0.ItemsStruc_0.ItemsEquiped <= 2) return;

        Form1_0.method_1("Grabbing all items for gold", Color.BlueViolet);
        Form1_0.WaitDelay(5);

        while (true)
        {
            Form1_0.PlayerScan_0.GetPositions();
            if (!GrabItemsForGold())
            {
                break;
            }
            else
            {
                if (ItemNAAME == LastGrabbedItem)
                {
                    TryGrabCount++;
                    if (TryGrabCount > 5)
                    {
                        break;
                    }
                }
                else
                {
                    TryGrabCount = 0;
                    ItemsGrabbed++;
                    if ((!CharConfig.UseTeleport && ItemsGrabbed > 2)
                        || (CharConfig.UseTeleport && ItemsGrabbed > 7))
                    {
                        break;
                    }
                }
                LastGrabbedItem = ItemNAAME;
            }
        }
    }

    public bool GrabItemsForGold()
    {
        try
        {
            Form1_0.PatternsScan_0.scanForUnitsPointer("item");

            foreach (var ThisCurrentPointer in Form1_0.PatternsScan_0.AllItemsPointers)
            {
                ItemPointerLocation = ThisCurrentPointer.Key;
                if (ItemPointerLocation > 0)
                {
                    itemdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(ItemPointerLocation, ref itemdatastruc, 144);
                    ItemNAAME = Form1_0.ItemsNames_0.getItemBaseName(BitConverter.ToUInt32(itemdatastruc, 4));
                    GetUnitData();
                    GetUnitPathData();
                    GetStatsAddr();

                    //; on ground, dropping
                    if (itemdatastruc[0x0C] == 3 || itemdatastruc[0x0C] == 5)
                    {
                        Form1_0.UIScan_0.readUI();
                        if (!Form1_0.UIScan_0.leftMenu && !Form1_0.UIScan_0.rightMenu && !Form1_0.UIScan_0.fullMenu)
                        {
                            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, itemx, itemy);
                            if (ShouldPickPos(itemScreenPos))
                            {
                                //####
                                int DiffXPlayer = itemx - Form1_0.PlayerScan_0.xPosFinal;
                                int DiffYPlayer = itemy - Form1_0.PlayerScan_0.yPosFinal;
                                if (DiffXPlayer < 0) DiffXPlayer = -DiffXPlayer;
                                if (DiffYPlayer < 0) DiffYPlayer = -DiffYPlayer;

                                if (DiffXPlayer > 100 || DiffYPlayer > 100)
                                {
                                    continue;
                                }

                                if (DiffXPlayer > 4
                                    || DiffYPlayer > 4)
                                {
                                    Form1_0.Mover_0.MoveToLocation(itemx, itemy);
                                    Form1_0.PlayerScan_0.GetPositions();
                                    GetUnitPathData();
                                    itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, itemx, itemy);
                                }
                                //####

                                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);

                                if (ItemNAAME != LastPick)
                                {
                                    LastPick = ItemNAAME;
                                    Form1_0.method_1("Grabbed for gold: " + ItemNAAME, GetColorFromQuality((int)itemQuality));
                                }
                                return true;
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            Form1_0.method_1("Couldn't 'GrabItemsForGold()'", Color.OrangeRed);
        }

        return false;
    }

    public bool ShouldPickPos(Position itemScreenPos)
    {
        if (CharConfig.UseTeleport && !Form1_0.Town_0.GetInTown())
        {
            if (itemScreenPos.X > 0 && itemScreenPos.Y > 0) return true;
            return false;

        }
        else
        {
            if (itemScreenPos.X > 0 && itemScreenPos.X < Form1_0.ScreenX
                && itemScreenPos.Y > 0 && itemScreenPos.Y < (Form1_0.ScreenY - Form1_0.ScreenYMenu))
            {
                return true;
            }
        }
        return false;
    }

    public void SetNumSockets()
    {
        numSockets = 0;
        if (isSocketed)
        {
            numSockets = GetValuesFromStats(Enums.Attribute.Sockets);
        }
    }

    public string GetAllValuesFromStats()
    {
        string AllStats = "";
        if (this.statCount > 0)
        {
            for (int i = 0; i < this.statCount; i++)
            {
                int offset = i * 8;
                ushort statLayer = BitConverter.ToUInt16(statBuffer, offset);
                ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);

                if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                    || statEnum == 216 || statEnum == 217 || statEnum == 217)
                {
                    if (statValue > 1000) statValue = statValue >> 8;
                }
                if (statEnum == 56 || statEnum == 59) statValue = statValue / 25;
                if (statEnum == 57 || statEnum == 58) statValue = (int)((double)statValue / 3.5);

                if (AllStats != "") AllStats += " && ";
                if (GetCustomStatsName(statEnum, statLayer) != "") AllStats += "[" + ((Enums.Attribute)statEnum) + "-" + GetCustomStatsName(statEnum, statLayer) + "] == " + statValue;
                else AllStats += "[" + ((Enums.Attribute)statEnum) + "] == " + statValue;
                //Form1_0.method_1("Item: " + ItemNAAME + ", Stat (" + ((Enums.Attribute) statEnum) + "):" + statValue, Color.Red);
            }
        }

        if (this.statExCount > 0)
        {
            for (int i = 0; i < this.statExCount; i++)
            {
                int offset = i * 8;
                ushort statLayer = BitConverter.ToUInt16(statBufferEx, offset);
                ushort statEnum = BitConverter.ToUInt16(statBufferEx, offset + 0x2);
                int statValue = BitConverter.ToInt32(statBufferEx, offset + 0x4);

                if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                    || statEnum == 216 || statEnum == 217)
                {
                    if (statValue > 1000) statValue = statValue >> 8;
                }
                if (statEnum == 56 || statEnum == 59) statValue = statValue / 25;
                if (statEnum == 57 || statEnum == 58) statValue = (int)((double)statValue / 3.5);

                if (AllStats != "") AllStats += " && ";
                if (GetCustomStatsName(statEnum, statLayer) != "") AllStats += "[" + ((Enums.Attribute)statEnum) + "-" + GetCustomStatsName(statEnum, statLayer) + "] == " + statValue;
                else AllStats += "[" + ((Enums.Attribute)statEnum) + "] == " + statValue;
                //Form1_0.method_1("Item: " + ItemNAAME + ", Stat (" + ((Enums.Attribute)statEnum) + "):" + statValue, Color.Red);
            }
        }

        return AllStats; // or some other default value
    }

    public int GetValuesFromStats(Enums.Attribute CheckTStat, ushort ThisLayer = 0)
    {
        if (this.statCount > 0)
        {
            for (int i = 0; i < this.statCount; i++)
            {
                int offset = i * 8;
                ushort statLayer = BitConverter.ToUInt16(statBuffer, offset);
                ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);

                if (statEnum == (ushort)CheckTStat)
                {
                    if (ThisLayer == 0 || (ThisLayer != 0 && statLayer == ThisLayer))
                    {
                        if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                            || statEnum == 216 || statEnum == 217)
                        {
                            if (statValue > 1000) return statValue >> 8;
                        }
                        if (statEnum == 56 || statEnum == 59) return statValue / 25;
                        if (statEnum == 57 || statEnum == 58) return (int)((double)statValue / 3.5);
                        return statValue;
                    }
                }
            }
        }

        if (this.statExCount > 0)
        {
            for (int i = 0; i < this.statExCount; i++)
            {
                int offset = i * 8;
                ushort statLayer = BitConverter.ToUInt16(statBufferEx, offset);
                ushort statEnum = BitConverter.ToUInt16(statBufferEx, offset + 0x2);
                int statValue = BitConverter.ToInt32(statBufferEx, offset + 0x4);

                if (statEnum == (ushort)CheckTStat)
                {
                    if (ThisLayer == 0 || (ThisLayer != 0 && statLayer == ThisLayer))
                    {
                        if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                            || statEnum == 216 || statEnum == 217)
                        {
                            if (statValue > 1000) return statValue >> 8;
                        }
                        if (statEnum == 56 || statEnum == 59) return statValue / 25;
                        if (statEnum == 57 || statEnum == 58) return (int)((double)statValue / 3.5);
                        return statValue;
                    }
                }
            }
        }

        return 0; // or some other default value
    }

    public string GetCustomStatsName(ushort statEnum, ushort statLayer)
    {
        string CustomName = "";
        if ((Enums.Attribute)statEnum == Enums.Attribute.NonClassSkill)
        {
            return ((Enums.NonClassSkill)statLayer).ToString();
        }
        if ((Enums.Attribute)statEnum == Enums.Attribute.AddClassSkills)
        {
            return ((Enums.AddClassSkills)statLayer).ToString();
        }
        if ((Enums.Attribute)statEnum == Enums.Attribute.AddSkillTab)
        {
            return ((Enums.AddSkillTab)statLayer).ToString();
        }
        if ((Enums.Attribute)statEnum == Enums.Attribute.SingleSkill)
        {
            return ((Enums.SingleSkill)statLayer).ToString();
        }
        if ((Enums.Attribute)statEnum == Enums.Attribute.Aura)
        {
            return ((Enums.Aura)statLayer).ToString();
        }
        return CustomName;
    }

    public bool GetIsCustomStats(Enums.Attribute statEnum)
    {
        if (statEnum == Enums.Attribute.NonClassSkill) return true;
        if (statEnum == Enums.Attribute.AddClassSkills) return true;
        if (statEnum == Enums.Attribute.AddSkillTab) return true;
        if (statEnum == Enums.Attribute.SingleSkill) return true;
        if (statEnum == Enums.Attribute.Aura) return true;

        return false;
    }

    public string GetQualityTextString()
    {
        return "[Quality] == " + getQuality((int)itemQuality);
    }

    public void setQuality(int qualityNo)
    {
        quality = getQuality(qualityNo);
    }

    public string getQuality(int qualityNo)
    {
        switch (qualityNo)
        {
            case 1:
                return "Inferior";
            case 2:
                return "Normal";
            case 3:
                return "Superior";
            case 4:
                return "Magic";
            case 5:
                return "Set";
            case 6:
                return "Rare";
            case 7:
                return "Unique";
            case 8:
                return "Crafted";
            case 9:
                return "Tempered";
            default:
                return "";
        }
    }

    public Color GetColorFromQuality(int qualityNo)
    {
        switch (qualityNo)
        {
            case 1:
                return Color.Black;
            case 2:
                return Color.Black; //should be white item, but white not visible
            case 3:
                return System.Drawing.ColorTranslator.FromHtml("#6a6a6a");
            case 4:
                return System.Drawing.ColorTranslator.FromHtml("#0005ff");
            case 5:
                return Color.Green;
            case 6:
                return Color.DarkGoldenrod;
            case 7:
                return System.Drawing.Color.FromArgb(255, 235, 187, 30);
            case 8:
                return System.Drawing.ColorTranslator.FromHtml("#fa5304");
            case 9:
                return Color.Red;
            default:
                return Color.Black;
        }
    }

    public bool IsItemHaveSameStatMultiCheck(string StatName, int StatValueToCheck, string ComparatorMethod)
    {
        if (StatName.Contains("+"))
        {
            string[] StatNames = StatName.Split('+');

            int TotalValue = 0;
            for (int i = 0; i < StatNames.Length; i++) TotalValue += GetValueFromStats(StatNames[i], GetStatLayerIndex(StatNames[i]));
            return IsValueTrue(ComparatorMethod, TotalValue, StatValueToCheck);
        }
        else
        {
            return IsItemHaveSameStat(StatName, StatValueToCheck, ComparatorMethod, GetStatLayerIndex(StatName));
        }
    }

    public int GetValueFromStats(string StatName, ushort ThisLayer = 0)
    {
        int EnumIndex = GetStatEnumIndex(StatName);
        if (EnumIndex > -1)
        {
            if (this.statCount > 0)
            {
                for (int i = 0; i < this.statCount; i++)
                {
                    int offset = i * 8;
                    ushort statLayer = BitConverter.ToUInt16(statBuffer, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);

                    if (statEnum == EnumIndex)
                    {
                        if (ThisLayer == 0 || (ThisLayer != 0 && statLayer == ThisLayer))
                        {
                            if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                                || statEnum == 216 || statEnum == 217)
                            {
                                if (statValue > 1000) return statValue >> 8;
                            }
                            if (statEnum == 56 || statEnum == 59) return statValue / 25;
                            if (statEnum == 57 || statEnum == 58) return (int)((double)statValue / 3.5);
                            return statValue;
                        }
                    }
                }
            }

            if (this.statExCount > 0)
            {
                for (int i = 0; i < this.statExCount; i++)
                {
                    int offset = i * 8;
                    ushort statLayer = BitConverter.ToUInt16(statBufferEx, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBufferEx, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBufferEx, offset + 0x4);

                    if (statEnum == EnumIndex)
                    {
                        if (ThisLayer == 0 || (ThisLayer != 0 && statLayer == ThisLayer))
                        {
                            if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                                || statEnum == 216 || statEnum == 217)
                            {
                                if (statValue > 1000) return statValue >> 8;
                            }
                            if (statEnum == 56 || statEnum == 59) return statValue / 25;
                            if (statEnum == 57 || statEnum == 58) return (int)((double)statValue / 3.5);
                            return statValue;
                        }
                    }
                }
            }
        }
        else
        {
            Form1_0.method_1("Error Stat '" + StatName + "' doesn't exist!", Color.Red);
        }

        return 0; //no identical stats found, return true by default
    }

    public bool IsItemHaveSameStat(string StatName, int StatValueToCheck, string ComparatorMethod, ushort ThisLayer = 0)
    {
        int EnumIndex = GetStatEnumIndex(StatName);
        if (EnumIndex > -1)
        {
            if (this.statCount > 0)
            {
                for (int i = 0; i < this.statCount; i++)
                {
                    int offset = i * 8;
                    ushort statLayer = BitConverter.ToUInt16(statBuffer, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);

                    if (statEnum == EnumIndex)
                    {
                        if (ThisLayer == 0 || (ThisLayer != 0 && statLayer == ThisLayer))
                        {
                            if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                                || statEnum == 216 || statEnum == 217)
                            {
                                if (statValue > 1000) statValue = statValue >> 8;
                            }
                            if (statEnum == 56 || statEnum == 59) statValue = statValue / 25;
                            if (statEnum == 57 || statEnum == 58) statValue = (int)((double)statValue / 3.5);
                        }

                        return IsValueTrue(ComparatorMethod, statValue, StatValueToCheck);
                    }
                }
            }

            if (this.statExCount > 0)
            {
                for (int i = 0; i < this.statExCount; i++)
                {
                    int offset = i * 8;
                    ushort statLayer = BitConverter.ToUInt16(statBufferEx, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBufferEx, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBufferEx, offset + 0x4);

                    if (statEnum == EnumIndex)
                    {
                        if (ThisLayer == 0 || (ThisLayer != 0 && statLayer == ThisLayer))
                        {
                            if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                            || statEnum == 216 || statEnum == 217)
                            {
                                if (statValue > 1000) statValue = statValue >> 8;
                            }
                            if (statEnum == 56 || statEnum == 59) statValue = statValue / 25;
                            if (statEnum == 57 || statEnum == 58) statValue = (int)((double)statValue / 3.5);
                        }

                        return IsValueTrue(ComparatorMethod, statValue, StatValueToCheck);
                    }
                }
            }
        }
        else
        {
            Form1_0.method_1("Error Stat '" + StatName + "' doesn't exist!", Color.Red);
        }

        return false; //no identical stats found, return true by default
    }

    public ushort GetStatLayerIndex(string StatNammm)
    {
        //Custom Stats
        foreach (int i in Enum.GetValues(typeof(Enums.NonClassSkill)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.NonClassSkill), i);
            if (EnumStr == StatNammm) return (ushort)i;
        }
        foreach (int i in Enum.GetValues(typeof(Enums.AddClassSkills)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.AddClassSkills), i);
            if (EnumStr == StatNammm) return (ushort)i;
        }
        foreach (int i in Enum.GetValues(typeof(Enums.AddSkillTab)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.AddSkillTab), i);
            if (EnumStr == StatNammm) return (ushort)i;
        }
        foreach (int i in Enum.GetValues(typeof(Enums.SingleSkill)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.SingleSkill), i);
            if (EnumStr == StatNammm) return (ushort)i;
        }
        foreach (int i in Enum.GetValues(typeof(Enums.Aura)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.Aura), i);
            if (EnumStr == StatNammm) return (ushort)i;
        }

        //Normal Stats
        foreach (int i in Enum.GetValues(typeof(Enums.Attribute)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.Attribute), i);
            if (EnumStr == StatNammm) return (ushort)i;
        }
        return 0;
    }

    public int GetStatEnumIndex(string StatNammm)
    {
        //Custom Stats
        foreach (int i in Enum.GetValues(typeof(Enums.NonClassSkill)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.NonClassSkill), i);
            if (EnumStr == StatNammm)
            {
                return 97;
            }
        }
        foreach (int i in Enum.GetValues(typeof(Enums.AddClassSkills)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.AddClassSkills), i);
            if (EnumStr == StatNammm)
            {
                return 83;
            }
        }
        foreach (int i in Enum.GetValues(typeof(Enums.AddSkillTab)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.AddSkillTab), i);
            if (EnumStr == StatNammm)
            {
                return 188;
            }
        }
        foreach (int i in Enum.GetValues(typeof(Enums.SingleSkill)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.SingleSkill), i);
            if (EnumStr == StatNammm)
            {
                return 107;
            }
        }
        foreach (int i in Enum.GetValues(typeof(Enums.Aura)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.Aura), i);
            if (EnumStr == StatNammm)
            {
                return 151;
            }
        }

        //Normal Stats
        int EnumIndexing = 0;
        int EnumIndex = -1;
        foreach (int i in Enum.GetValues(typeof(Enums.Attribute)))
        {
            string EnumStr = Enum.GetName(typeof(Enums.Attribute), i);
            if (EnumStr == StatNammm)
            {
                EnumIndex = EnumIndexing;
                break;
            }
            EnumIndexing++;
        }
        return EnumIndex;
    }

    public int GetStatValue(int ThisEnum, ushort ThisLayer = 0)
    {
        if (ThisEnum == -1) return -1;

        if (this.statCount > 0)
        {
            for (int i = 0; i < this.statCount; i++)
            {
                int offset = i * 8;
                ushort statLayer = BitConverter.ToUInt16(statBuffer, offset);
                ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);

                if (statEnum == ThisEnum)
                {
                    if (ThisLayer == 0 || (ThisLayer != 0 && statLayer == ThisLayer))
                    {
                        if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                            || statEnum == 216 || statEnum == 217)
                        {
                            if (statValue > 1000) return statValue >> 8;
                        }
                        if (statEnum == 56 || statEnum == 59) return statValue / 25;
                        if (statEnum == 57 || statEnum == 58) return (int)((double)statValue / 3.5);
                        return statValue;
                    }
                }
            }
        }

        if (this.statExCount > 0)
        {
            for (int i = 0; i < this.statExCount; i++)
            {
                int offset = i * 8;
                ushort statLayer = BitConverter.ToUInt16(statBufferEx, offset);
                ushort statEnum = BitConverter.ToUInt16(statBufferEx, offset + 0x2);
                int statValue = BitConverter.ToInt32(statBufferEx, offset + 0x4);

                if (statEnum == ThisEnum)
                {
                    if (ThisLayer == 0 || (ThisLayer != 0 && statLayer == ThisLayer))
                    {
                        if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                            || statEnum == 216 || statEnum == 217)
                        {
                            if (statValue > 1000) return statValue >> 8;
                        }
                        if (statEnum == 56 || statEnum == 59) return statValue / 25;
                        if (statEnum == 57 || statEnum == 58) return (int)((double)statValue / 3.5);
                        return statValue;
                    }
                }
            }
        }

        return -1;
    }

    public bool IsValueTrue(string ComparatorMethod, int CurrentV, int CheckingV)
    {
        if (ComparatorMethod == "==")
        {
            if (CurrentV == CheckingV)
            {
                return true;
            }
        }
        if (ComparatorMethod == "<=")
        {
            if (CurrentV <= CheckingV)
            {
                return true;
            }
        }
        if (ComparatorMethod == ">=")
        {
            if (CurrentV >= CheckingV)
            {
                return true;
            }
        }
        if (ComparatorMethod == "<")
        {
            if (CurrentV < CheckingV)
            {
                return true;
            }
        }
        if (ComparatorMethod == ">")
        {
            if (CurrentV > CheckingV)
            {
                return true;
            }
        }
        if (ComparatorMethod == "!=")
        {
            if (CurrentV != CheckingV)
            {
                return true;
            }
        }
        return false;
    }
}
