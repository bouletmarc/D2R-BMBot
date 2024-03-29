using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static app.Enums;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace app
{
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
        public bool UsePotionNotInRightSpot = true;


        public void SetForm1(Form1 Form1_1)
        {
            Form1_0 = Form1_1;
        }

        public void GetStatsAddr()
        {
            long pStatsListExPtr = BitConverter.ToInt64(itemdatastruc, 0x88);

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

            if (this.statCount > 0)
            {
                statBuffer = new byte[this.statCount * 10];
                Form1_0.Mem_0.ReadRawMemory(this.statPtr, ref statBuffer, (int)(this.statCount * 10));
            }
            else
            {
                statBuffer = new byte[] { };
            }
            if (this.statExCount > 0)
            {
                statBufferEx = new byte[this.statExCount * 10];
                Form1_0.Mem_0.ReadRawMemory(this.statExPtr, ref statBufferEx, (int)(this.statExCount * 10));
            }
            else
            {
                statBufferEx = new byte[] { };
            }

            SetNumSockets();

            //string SavePathh = Form1_0.ThisEndPath + "DumpItempStatBStruc";
            //File.Create(SavePathh).Dispose();
            //File.WriteAllBytes(SavePathh, pStatB);
        }

        public void GetUnitData()
        {
            pUnitDataPtr = BitConverter.ToInt64(itemdatastruc, 0x10);

            /*pUnitData = new byte[0x56];
            Form1_0.Mem_0.ReadRawMemory(pUnitDataPtr, ref pUnitData, pUnitData.Length);
            itemQuality = BitConverter.ToUInt32(pUnitData, 0x00);
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
            Form1_0.PatternsScan_0.scanForUnitsPointer("item");
            for (int i = 0; i < Form1_0.PatternsScan_0.AllItemsPointers.Count; i++)
            {
                ItemPointerLocation = Form1_0.PatternsScan_0.AllItemsPointers[i];
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
                        //if (dwOwnerId == Form1_0.PlayerScan_0.unitId)
                        if (dwOwnerId != Form1_0.PlayerScan_0.unitId && equiploc == 3)
                        {
                            if (ItemNAAME == ShopItemName)
                            {
                                //SetStats();
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool GetItems(bool IsPickingItem)
        {
            if (!Form1_0.GameStruc_0.IsInGame()) return false;

            //dead leave game
            if (Form1_0.PlayerScan_0.PlayerDead || Form1_0.Potions_0.ForceLeave)
            {
                Form1_0.Potions_0.ForceLeave = true;
                Form1_0.Baal_0.SearchSameGamesAsLastOne = false;
                Form1_0.LeaveGame(false);
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
                ItemOnCursor = false;
            }

            Form1_0.PatternsScan_0.scanForUnitsPointer("item");

            for (int i = 0; i < Form1_0.PatternsScan_0.AllItemsPointers.Count; i++)
            {
                ItemPointerLocation = Form1_0.PatternsScan_0.AllItemsPointers[i];
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

                    if (itemdatastruc[0x0C] == 4)
                    {
                        ItemOnCursor = true;
                        //Form1_0.method_1("cursor: " + ItemNAAME + " - at: " + itemx + "," + itemy, Color.BlueViolet);
                    }

                    if (itemdatastruc[0x0C] == 0)
                    {
                        if (dwOwnerId == Form1_0.PlayerScan_0.unitId && equiploc == 0)
                        {
                            ItemsInInventory++;
                            //Form1_0.method_1("inv: " + ItemNAAME + " - at: " + itemx + "," + itemy);
                            if (!IsPickingItem)
                            {
                                Form1_0.PlayerScan_0.GetHPAndManaOnThisEquippedItem();
                                Form1_0.InventoryStruc_0.SetInventoryItem();
                                Form1_0.InventoryStruc_0.SetHUDItem();
                            }
                        }

                        if (dwOwnerId == Form1_0.PlayerScan_0.unitId && equiploc == 4)
                        {
                            //here for items in stash
                            //Form1_0.method_1("name: " + ItemNAAME + " - at: " + itemx + "," + itemy, Color.DarkGreen);
                            Form1_0.StashStruc_0.AddStashItem(itemx, itemy, 1);
                        }
                        if (dwOwnerId != Form1_0.PlayerScan_0.unitId && equiploc == 4)
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
                            }
                        }
                        if (dwOwnerId == Form1_0.PlayerScan_0.unitId && equiploc == 3)
                        {
                            //here for items in cube
                            //Form1_0.method_1("name: " + ItemNAAME + " - at: " + itemx + "," + itemy, Color.DarkGreen);
                            Form1_0.Cubing_0.AddCubeItem(itemx, itemy);
                        }
                    }
                    if (itemdatastruc[0x0C] == 1)
                    {
                        if (dwOwnerId == Form1_0.PlayerScan_0.unitId && equiploc == 255)
                        {
                            ItemsEquiped++;
                            if (!IsPickingItem)
                            {
                                Form1_0.PlayerScan_0.GetHPAndManaOnThisEquippedItem();
                                Form1_0.Repair_0.GetDurabilityOnThisEquippedItem();
                            }
                            //Form1_0.method_1("name: " + ItemNAAME + " - at: " + itemx + "," + itemy + " - " + equiploc, Color.DarkGreen);
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
                        }
                    }
                    //; on ground, dropping
                    if (itemdatastruc[0x0C] == 3 || itemdatastruc[0x0C] == 5)
                    {
                        ItemsOnGround++;

                        Form1_0.UIScan_0.readUI();
                        if (Form1_0.ItemsAlert_0.ShouldPickItem(false) || Form1_0.BeltStruc_0.ItemGrabPotion()
                            && (!Form1_0.UIScan_0.leftMenu && !Form1_0.UIScan_0.rightMenu && !Form1_0.UIScan_0.fullMenu)
                            && IsPickingItem)
                        {
                            if (ItemNAAME == "Perfect Diamond" && (Form1_0.PlayerScan_0.levelNo >= 106 && Form1_0.PlayerScan_0.levelNo < 109)) continue;

                            //Form1_0.method_1("Name: " + ItemNAAME, Color.DarkViolet);


                            //Console.WriteLine("Pointer Addr: " + ItemPointerLocation.ToString("X"));
                            //Console.WriteLine("Path Addr: " + pPathPtr.ToString("X"));

                            /*Console.WriteLine("Path Addr: " + Form1_0.Mem_0.ReadByteRaw((IntPtr)(pPathPtr + 0x20)).ToString("X"));

                            string SavePathh = Form1_0.ThisEndPath + "DumpItempPathStruc";
                            File.Create(SavePathh).Dispose();
                            File.WriteAllBytes(SavePathh, itemdatastruc);

                            SavePathh = Form1_0.ThisEndPath + "DumpItempPathStruc2";
                            File.Create(SavePathh).Dispose();
                            File.WriteAllBytes(SavePathh, pPath);*/

                            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, itemx, itemy);
                            if (ShouldPickPos(itemScreenPos))
                            {
                                int DiffXPlayer = itemx - Form1_0.PlayerScan_0.xPosFinal;
                                int DiffYPlayer = itemy - Form1_0.PlayerScan_0.yPosFinal;
                                if (DiffXPlayer < 0) DiffXPlayer = -DiffXPlayer;
                                if (DiffYPlayer < 0) DiffYPlayer = -DiffYPlayer;

                                if (DiffXPlayer > 1000 || DiffYPlayer > 1000)
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
                                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);

                                if (ItemNAAME != LastPick)
                                {
                                    LastPick = ItemNAAME;
                                    Form1_0.method_1_Items("Picked: " + ItemNAAME, GetColorFromQuality((int) itemQuality));

                                    Form1_0.BeltStruc_0.ItemIsPotion();
                                    if (Form1_0.BeltStruc_0.IsItemHPPotion
                                        || Form1_0.BeltStruc_0.IsItemManaPotion
                                        || Form1_0.BeltStruc_0.IsItemRVPotion
                                        || Form1_0.BeltStruc_0.IsItemFullRVPotion)
                                    {
                                        Form1_0.BeltStruc_0.CheckForMissingPotions();
                                    }

                                    //string SavePathh = Form1_0.ThisEndPath + "DumpItemDataStruc";
                                    //File.Create(SavePathh).Dispose();
                                    //File.WriteAllBytes(SavePathh, itemdatastruc);
                                }
                                return true;
                            }
                        }
                    }
                }
            }

            //Form1_0.method_1("-----", Color.Black);
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
            string LastGrabbedItem = "";
            int TryGrabCount = 0;
            int ItemsGrabbed = 0;

            if (Form1_0.ItemsStruc_0.ItemsEquiped <= 2) return;

            Form1_0.method_1("Grabbing all items for gold", Color.BlueViolet);

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
            Form1_0.PatternsScan_0.scanForUnitsPointer("item");

            long ItemPointMaxValue = 0;
            int ItemHighestValue = 0;

            for (int i = 0; i < Form1_0.PatternsScan_0.AllItemsPointers.Count; i++)
            {
                ItemPointerLocation = Form1_0.PatternsScan_0.AllItemsPointers[i];
                if (ItemPointerLocation > 0)
                {
                    itemdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(ItemPointerLocation, ref itemdatastruc, 144);
                    GetStatsAddr();
                    GetUnitPathData();
                    int ItemValue = GetValuesFromStats(Enums.Attribute.Value);

                    //; on ground, dropping
                    if (itemdatastruc[0x0C] == 3 || itemdatastruc[0x0C] == 5)
                    {
                        if (itemx > 0 && itemy > 0)
                        {
                            if (itemx - Form1_0.PlayerScan_0.xPosFinal > 50
                                || itemx - Form1_0.PlayerScan_0.xPosFinal < -50
                                || itemy - Form1_0.PlayerScan_0.yPosFinal > 50
                                || itemy - Form1_0.PlayerScan_0.yPosFinal < -50)
                            {
                                continue;
                            }

                            Form1_0.UIScan_0.readUI();
                            if (!Form1_0.UIScan_0.leftMenu && !Form1_0.UIScan_0.rightMenu && !Form1_0.UIScan_0.fullMenu)
                            {
                                if (ItemValue >= ItemHighestValue)
                                {
                                    ItemHighestValue = ItemValue;
                                    ItemPointMaxValue = ItemPointerLocation;
                                }
                            }
                        }
                    }
                }
            }

            //clic highest value item
            if (ItemPointMaxValue > 0)
            {
                ItemPointerLocation = ItemPointMaxValue;
                itemdatastruc = new byte[144];
                Form1_0.Mem_0.ReadRawMemory(ItemPointerLocation, ref itemdatastruc, 144);
                ItemNAAME = Form1_0.ItemsNames_0.getItemBaseName(BitConverter.ToUInt32(itemdatastruc, 4));
                GetUnitPathData();
                //; on ground, dropping
                if (itemdatastruc[0x0C] == 3 || itemdatastruc[0x0C] == 5)
                {
                    Form1_0.UIScan_0.readUI();
                    if (!Form1_0.UIScan_0.leftMenu && !Form1_0.UIScan_0.rightMenu && !Form1_0.UIScan_0.fullMenu)
                    {
                        Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, itemx, itemy);
                        if (ShouldPickPos(itemScreenPos))
                        {
                            //####
                            int DiffXPlayer = itemx - Form1_0.PlayerScan_0.xPosFinal;
                            int DiffYPlayer = itemy - Form1_0.PlayerScan_0.yPosFinal;
                            if (DiffXPlayer < 0) DiffXPlayer = -DiffXPlayer;
                            if (DiffYPlayer < 0) DiffYPlayer = -DiffYPlayer;

                            if (DiffXPlayer > 4
                                || DiffYPlayer > 4)
                            {
                                Form1_0.Mover_0.MoveToLocation(itemx, itemy);
                                Form1_0.PlayerScan_0.GetPositions();
                                GetUnitPathData();
                                itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, itemx, itemy);
                            }
                            //####
                            Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);

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

            return false;
        }

        public bool ShouldPickPos(Dictionary<string, int> itemScreenPos)
        {
            if (CharConfig.UseTeleport && !Form1_0.Town_0.GetInTown())
            {
                if (itemScreenPos["x"] > 0 && itemScreenPos["y"] > 0) return true;
                return false;

            }
            else
            {
                if (itemScreenPos["x"] > 0 && itemScreenPos["x"] < Form1_0.ScreenX
                    && itemScreenPos["y"] > 0 && itemScreenPos["y"] < (Form1_0.ScreenY - Form1_0.ScreenYMenu))
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
                numSockets = GetValuesFromStats(Enums.Attribute.NumSockets);
            }
        }

        public int GetValuesFromStats(Enums.Attribute CheckTStat)
        {
            if (this.statCount > 0)
            {
                for (int i = 0; i < this.statCount; i++)
                {
                    int offset = i * 8;
                    //short statLayer = BitConverter.ToInt16(statBuffer, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);

                    if (statEnum == (ushort)CheckTStat)
                    {
                        if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                            || statEnum == 216 || statEnum == 217)
                        {
                            return statValue >> 8;
                        }
                        if (statEnum == 56 || statEnum == 59)
                        {
                            return statValue / 25;
                        }
                        return statValue;
                    }
                }
            }

            if (this.statExCount > 0)
            {
                for (int i = 0; i < this.statExCount; i++)
                {
                    int offset = i * 8;
                    //short statLayer = BitConverter.ToInt16(statBufferEx, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBufferEx, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBufferEx, offset + 0x4);

                    if (statEnum == (ushort)CheckTStat)
                    {
                        if (statEnum == 6 || statEnum == 7 || statEnum == 8 || statEnum == 9 || statEnum == 10 || statEnum == 11
                            || statEnum == 216 || statEnum == 217)
                        {
                            return statValue >> 8;
                        }
                        if (statEnum == 56 || statEnum == 59)
                        {
                            return statValue / 25;
                        }
                        return statValue;
                    }
                }
            }

            return 0; // or some other default value
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
                    return System.Drawing.ColorTranslator.FromHtml("#9c6d2a");
                case 8:
                    return System.Drawing.ColorTranslator.FromHtml("#fa5304");
                case 9:
                    return Color.Red;
                default:
                    return Color.Black;
            }
        }

        public bool IsItemHaveSameStatMulti(string[] StatNames, int StatValueToCheck, string ComparatorMethod)
        {
            int[] statValues = new int[StatNames.Length];
            for (int i = 0; i < StatNames.Length; i++)
            {
                statValues[i] = GetStatValue(GetStatEnumIndex(StatNames[i]));
            }

            bool HasBadValue = false;
            for (int i = 0; i < statValues.Length; i++)
            {
                if (statValues[i] == -1)
                {
                    HasBadValue = true;
                }
            }

            if (!HasBadValue)
            {
                int FinalValue = 0;
                for (int i = 0; i < statValues.Length; i++)
                {
                    FinalValue += statValues[i];
                }

                return IsValueTrue(ComparatorMethod, FinalValue, StatValueToCheck);
            }

            return true; //no identical stats found, return true by default
        }

        public bool IsItemHaveSameStat(string StatName, int StatValueToCheck, string ComparatorMethod)
        {
            int EnumIndex = GetStatEnumIndex(StatName);
            if (EnumIndex > -1)
            {
                if (this.statCount > 0)
                {
                    for (int i = 0; i < this.statCount; i++)
                    {
                        int offset = i * 8;
                        //short statLayer = BitConverter.ToInt16(statBuffer, offset);
                        ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                        int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);

                        if (statEnum == EnumIndex)
                        {
                            return IsValueTrue(ComparatorMethod, statValue, StatValueToCheck);
                        }
                    }
                }

                if (this.statExCount > 0)
                {
                    for (int i = 0; i < this.statExCount; i++)
                    {
                        int offset = i * 8;
                        //short statLayer = BitConverter.ToInt16(statBufferEx, offset);
                        ushort statEnum = BitConverter.ToUInt16(statBufferEx, offset + 0x2);
                        int statValue = BitConverter.ToInt32(statBufferEx, offset + 0x4);

                        if (statEnum == EnumIndex)
                        {
                            return IsValueTrue(ComparatorMethod, statValue, StatValueToCheck);
                        }
                    }
                }
            }

            return false; //no identical stats found, return true by default
        }

        public int GetStatEnumIndex(string StatNammm)
        {
            int EnumIndexing = 0;
            int EnumIndex = -1;
            foreach (int i in Enum.GetValues(typeof(Enums.Attribute)))
            {
                string EnumStr = Enum.GetName(typeof(Enums.Attribute), i);
                if (EnumStr.ToLower() == StatNammm.ToLower())
                {
                    EnumIndex = EnumIndexing;
                    break;
                }
                EnumIndexing++;
            }
            return EnumIndex;
        }

        public int GetStatValue(int ThisEnum)
        {
            if (ThisEnum == -1) return -1;

            if (this.statCount > 0)
            {
                for (int i = 0; i < this.statCount; i++)
                {
                    int offset = i * 8;
                    //short statLayer = BitConverter.ToInt16(statBuffer, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);

                    if (statEnum == ThisEnum)
                    {
                        return statValue;
                    }
                }
            }

            if (this.statExCount > 0)
            {
                for (int i = 0; i < this.statExCount; i++)
                {
                    int offset = i * 8;
                    //short statLayer = BitConverter.ToInt16(statBufferEx, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBufferEx, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBufferEx, offset + 0x4);

                    if (statEnum == ThisEnum)
                    {
                        return statValue;
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
}
