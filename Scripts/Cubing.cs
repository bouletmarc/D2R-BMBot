using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static app.Enums;

namespace app
{
    public class Cubing
    {
        Form1 Form1_0;

        public List<string> CubingRecipes = new List<string>();
        public List<string> CurrentRecipe = new List<string>();
        public string CurrentRecipeResult = "";
        public List<int> CurrentRecipeItemInStashNumber = new List<int>();
        public List<int> CurrentRecipeItemLocations = new List<int>();

        public uint[] Cube_ItemTxtNoList = new uint[12];

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;

            /*CubingRecipes.Add("Flawless Emerald+Flawless Emerald+Flawless Emerald=Perfect Emerald");    //Perfect emerald
            CubingRecipes.Add("Flawless Sapphire+Flawless Sapphire+Flawless Sapphire=Perfect Sapphire"); //Perfect Sapphire
            CubingRecipes.Add("Flawless Amethyst+Flawless Amethyst+Flawless Amethyst=Perfect Amethyst"); //Perfect Amethyst
            CubingRecipes.Add("Flawless Topaz+Flawless Topaz+Flawless Topaz=Perfect Topaz");          //Perfect Topaz
            CubingRecipes.Add("Flawless Ruby+Flawless Ruby+Flawless Ruby=Perfect Ruby");             //Perfect Ruby
            CubingRecipes.Add("Flawless Diamond+Flawless Diamond+Flawless Diamond=Perfect Diamond");    //Perfect Diamond

            CubingRecipes.Add("El Rune+El Rune+El Rune=Eld Rune");      //Eld
            CubingRecipes.Add("Eld Rune+Eld Rune+Eld Rune=Tir Rune");   //Tir
            CubingRecipes.Add("Tir Rune+Tir Rune+Tir Rune=Nef Rune");   //Nef
            CubingRecipes.Add("Nef Rune+Nef Rune+Nef Rune=Eth Rune");   //Eth
            CubingRecipes.Add("Eth Rune+Eth Rune+Eth Rune=Ith Rune");   //Ith
            CubingRecipes.Add("Ith Rune+Ith Rune+Ith Rune=Tal Rune");   //Tal
            CubingRecipes.Add("Tal Rune+Tal Rune+Tal Rune=Ral Rune");   //Ral
            CubingRecipes.Add("Ral Rune+Ral Rune+Ral Rune=Ort Rune");   //Ort
            CubingRecipes.Add("Ort Rune+Ort Rune+Ort Rune=Thul Rune");   //Thul

            CubingRecipes.Add("Thul Rune+Thul Rune+Thul Rune+Chipped Topaz=Amn Rune");   //amn
            CubingRecipes.Add("Amn Rune+Amn Rune+Amn Rune+Chipped Amethyst=Sol Rune");   //sol
            CubingRecipes.Add("Sol Rune+Sol Rune+Sol Rune+Chipped Sapphire=Shael Rune"); //shael
            CubingRecipes.Add("Shael Rune+Shael Rune+Shael Rune+Chipped Ruby=Dol Rune"); //dol
            CubingRecipes.Add("Dol Rune+Dol Rune+Dol Rune+Chipped Emerald=Hel Rune");    //hel
            CubingRecipes.Add("Hel Rune+Hel Rune+Hel Rune+Chipped Diamond=Io Rune");     //Io

            CubingRecipes.Add("Io Rune+Io Rune+Io Rune+Flawed Topaz=Lum Rune");          //Lum
            CubingRecipes.Add("Lum Rune+Lum Rune+Lum Rune+Flawed Amethyst=Ko Rune");     //Ko
            CubingRecipes.Add("Ko Rune+Ko Rune+Ko Rune+Flawed Sapphire=Fal Rune");       //Fal
            CubingRecipes.Add("Fal Rune+Fal Rune+Fal Rune+Flawed Ruby=Lem Rune");        //Lem
            CubingRecipes.Add("Lem Rune+Lem Rune+Lem Rune+Flawed Emerald=Pul Rune");     //Pul
            CubingRecipes.Add("Pul Rune+Pul Rune+Flawed Diamond=Um Rune");               //Um

            //Token
            CubingRecipes.Add("Twisted Essence of Suffering+Charged Essense of Hatred+Burning Essence of Terror+Festering Essence of Destruction=Token of Absolution");*/
        }

        public void ResetCubeInventory()
        {
            Cube_ItemTxtNoList = new uint[12];
        }

        public void AddCubeItem(int PosX, int PosY)
        {
            int AtI = ConvertXYToFullCubeIndex(PosX, PosY);
            Cube_ItemTxtNoList[AtI] = Form1_0.ItemsStruc_0.txtFileNo;
        }

        public int ConvertXYToFullCubeIndex(int PosX, int PosY)
        {
            return PosX + (PosY * 3);
        }

        public void GetRecipeAt(int ThisI)
        {
            CurrentRecipeResult = "";
            CurrentRecipe = new List<string>();
            CurrentRecipeItemInStashNumber = new List<int>();
            CurrentRecipeItemLocations = new List<int>();

            string ThisRecipe = CubingRecipes[ThisI];
            if (ThisRecipe.Contains("+"))
            {
                string[] SplittedItemNames = ThisRecipe.Split('+');
                for (int i = 0; i < SplittedItemNames.Length; i++)
                {
                    if (SplittedItemNames[i].Contains("="))
                    {
                        string[] Splitt = SplittedItemNames[i].Split('=');
                        CurrentRecipe.Add(Splitt[0]);
                        CurrentRecipeResult = Splitt[1];
                    }
                    else
                    {
                        CurrentRecipe.Add(SplittedItemNames[i]);
                    }
                }
            }
        }

        public bool IsNotSameLocation(int ThisStash, int ThisLoc)
        {
            if (CurrentRecipeItemLocations.Count > 0)
            {
                for (int i = 0; i < CurrentRecipeItemLocations.Count; i++)
                {
                    if (CurrentRecipeItemLocations[i] == ThisLoc
                        && CurrentRecipeItemInStashNumber[i] == ThisStash)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool StashContainRecipeItem(string ItemName)
        {
            for (int i = 0; i < 100; i++)
            {
                if (Form1_0.StashStruc_0.Stash1_ItemTxtNoList[i] > 0)
                {
                    if (Form1_0.ItemsNames_0.getItemBaseName(Form1_0.StashStruc_0.Stash1_ItemTxtNoList[i]) == ItemName
                        && IsNotSameLocation(1, i))
                    {
                        CurrentRecipeItemLocations.Add(i);
                        CurrentRecipeItemInStashNumber.Add(1);
                        return true;
                    }
                }
            }
            for (int i = 0; i < 100; i++)
            {
                if (Form1_0.StashStruc_0.Stash2_ItemTxtNoList[i] > 0)
                {
                    if (Form1_0.ItemsNames_0.getItemBaseName(Form1_0.StashStruc_0.Stash2_ItemTxtNoList[i]) == ItemName
                        && IsNotSameLocation(2, i))
                    {
                        CurrentRecipeItemLocations.Add(i);
                        CurrentRecipeItemInStashNumber.Add(2);
                        return true;
                    }
                }
            }
            for (int i = 0; i < 100; i++)
            {
                if (Form1_0.StashStruc_0.Stash3_ItemTxtNoList[i] > 0)
                {
                    if (Form1_0.ItemsNames_0.getItemBaseName(Form1_0.StashStruc_0.Stash3_ItemTxtNoList[i]) == ItemName
                        && IsNotSameLocation(3, i))
                    {
                        CurrentRecipeItemLocations.Add(i);
                        CurrentRecipeItemInStashNumber.Add(3);
                        return true;
                    }
                }
            }
            for (int i = 0; i < 100; i++)
            {
                if (Form1_0.StashStruc_0.Stash4_ItemTxtNoList[i] > 0)
                {
                    if (Form1_0.ItemsNames_0.getItemBaseName(Form1_0.StashStruc_0.Stash4_ItemTxtNoList[i]) == ItemName
                        && IsNotSameLocation(4, i))
                    {
                        CurrentRecipeItemLocations.Add(i);
                        CurrentRecipeItemInStashNumber.Add(4);
                        return true;
                    }
                }
            }
            return false;
        }

        public void PerformCubing()
        {
            //get stash item
            if (Form1_0.ItemsStruc_0.dwOwnerId_Shared1 == 0 || Form1_0.ItemsStruc_0.dwOwnerId_Shared2 == 0 || Form1_0.ItemsStruc_0.dwOwnerId_Shared3 == 0)
            {
                Form1_0.ItemsStruc_0.GetItems(false);
                Form1_0.ItemsStruc_0.GetItems(false);

                //still zero return error
                if (Form1_0.ItemsStruc_0.dwOwnerId_Shared1 == 0 || Form1_0.ItemsStruc_0.dwOwnerId_Shared2 == 0 || Form1_0.ItemsStruc_0.dwOwnerId_Shared3 == 0)
                {
                    return;
                }
            }

            //loop thru all recipes
            for (int i = 0; i < CubingRecipes.Count; i++)
            {
                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }

                GetRecipeAt(i);

                bool CanCube = true;
                for (int k = 0; k < CurrentRecipe.Count; k++)
                {
                    if (!StashContainRecipeItem(CurrentRecipe[k]))
                    {
                        CanCube = false;
                        break;
                    }
                }

                //perform cubing
                if (CanCube)
                {
                    Form1_0.SetGameStatus("TOWN-STASH-CUBING:" + CurrentRecipeResult);
                    SendItemsToCube();
                }
            }
        }

        public void SendItemsToCube()
        {
            for (int i = 0; i < CurrentRecipeItemInStashNumber.Count; i++)
            {
                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }

                //select the stash where the item is located
                if (CurrentRecipeItemInStashNumber[i] == 1)
                {
                    Form1_0.KeyMouse_0.MouseClicc(200, 200);   //clic stash1
                }
                if (CurrentRecipeItemInStashNumber[i] == 2)
                {
                    Form1_0.KeyMouse_0.MouseClicc(340, 200);   //clic shared stash1
                }
                if (CurrentRecipeItemInStashNumber[i] == 3)
                {
                    Form1_0.KeyMouse_0.MouseClicc(450, 200);   //clic shared stash2
                }
                if (CurrentRecipeItemInStashNumber[i] == 4)
                {
                    Form1_0.KeyMouse_0.MouseClicc(600, 200);   //clic shared stash3
                }
                Form1_0.WaitDelay(15);

                //select the item
                Dictionary<string, int> itemScreenPos = ConvertIndexToXY(CurrentRecipeItemLocations[i]);
                itemScreenPos = ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);
                Form1_0.Stash_0.PickItem(itemScreenPos["x"], itemScreenPos["y"]);
                //Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                //Form1_0.WaitDelay(10);

                //select the stash where the cube is located
                if (Form1_0.StashStruc_0.CubeStashNumber == 1)
                {
                    Form1_0.KeyMouse_0.MouseClicc(200, 200);   //clic stash1
                }
                if (Form1_0.StashStruc_0.CubeStashNumber == 2)
                {
                    Form1_0.KeyMouse_0.MouseClicc(340, 200);   //clic shared stash1
                }
                if (Form1_0.StashStruc_0.CubeStashNumber == 3)
                {
                    Form1_0.KeyMouse_0.MouseClicc(450, 200);   //clic shared stash2
                }
                if (Form1_0.StashStruc_0.CubeStashNumber == 4)
                {
                    Form1_0.KeyMouse_0.MouseClicc(600, 200);   //clic shared stash3
                }
                Form1_0.WaitDelay(15);

                //clic on cube to send item to cube
                itemScreenPos = ConvertIndexToXY(Form1_0.StashStruc_0.CubeIndex);
                itemScreenPos = ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);
                Form1_0.Stash_0.PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
                Form1_0.Stash_0.PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
                //Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                //Form1_0.WaitDelay(40);
            }

            //clic on cube to open the cube
            Dictionary<string, int> itemScreenPos2 = ConvertIndexToXY(Form1_0.StashStruc_0.CubeIndex);
            itemScreenPos2 = ConvertInventoryLocToScreenPos(itemScreenPos2["x"], itemScreenPos2["y"]);

            Form1_0.UIScan_0.readUI();
            int tryyyy = 0;
            while (!Form1_0.UIScan_0.cubeMenu && tryyyy < 7)
            {
                Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos2["x"], itemScreenPos2["y"]);
                Form1_0.WaitDelay(25);
                Form1_0.ItemsStruc_0.GetItems(false);
                Form1_0.Stash_0.PlaceItem(itemScreenPos2["x"], itemScreenPos2["y"]);
                Form1_0.UIScan_0.readUI();
                tryyyy++;
            }

            if (Form1_0.UIScan_0.cubeMenu)
            {
                //clic transmute button
                Form1_0.KeyMouse_0.MouseClicc(405, 615);
                Form1_0.WaitDelay(35);
                Form1_0.KeyMouse_0.MouseClicc(405, 615);
                Form1_0.WaitDelay(35);
                Form1_0.KeyMouse_0.MouseClicc(405, 615);
                Form1_0.WaitDelay(35);
                Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again

                //send item to inventory
                Form1_0.UIScan_0.readUI();
                for (int i = 0; i < Cube_ItemTxtNoList.Length; i++)
                {
                    if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                    {
                        break;
                    }
                    if (Cube_ItemTxtNoList[i] != 0)
                    {
                        if (Form1_0.ItemsNames_0.getItemBaseName(Cube_ItemTxtNoList[i]) == CurrentRecipeResult)
                        {
                            Form1_0.method_1_Items("Cubed: " + Form1_0.ItemsNames_0.getItemBaseName(Cube_ItemTxtNoList[i]), Color.BlueViolet);
                        }
                    }

                    int tryyy = 0;
                    while (Cube_ItemTxtNoList[i] != 0 && tryyy < 10)
                    {
                        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                        {
                            break;
                        }

                        itemScreenPos2 = ConvertIndexToCubeXY(i);
                        itemScreenPos2 = ConvertCubeLocToScreenPos(itemScreenPos2["x"], itemScreenPos2["y"]);
                        Form1_0.KeyMouse_0.SendCTRL_CLICK(itemScreenPos2["x"], itemScreenPos2["y"]);
                        Form1_0.WaitDelay(5);
                        //Form1_0.KeyMouse_0.MouseClicc(itemScreenPos2["x"], itemScreenPos2["y"]);
                        //Form1_0.WaitDelay(10);
                        Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                        Form1_0.Stash_0.PlaceItem(itemScreenPos2["x"], itemScreenPos2["y"]);
                        tryyy++;
                    }
                }
            }

            Form1_0.UIScan_0.CloseUIMenu("cubeMenu");
        }

        public Dictionary<string, int> ConvertInventoryLocToScreenPos(int ThisX, int ThisY)
        {
            //starting at 1295,580 on screen for first item in inv, increment for 48px
            int xS = 185 + (ThisX * 48);
            int yS = 245 + (ThisY * 48);

            Dictionary<string, int> NewDict = new Dictionary<string, int>();
            NewDict["x"] = xS;
            NewDict["y"] = yS;
            return NewDict;
        }

        public Dictionary<string, int> ConvertCubeLocToScreenPos(int ThisX, int ThisY)
        {
            //starting at 1295,580 on screen for first item in inv, increment for 48px
            int xS = 360 + (ThisX * 48);
            int yS = 395 + (ThisY * 48);

            Dictionary<string, int> NewDict = new Dictionary<string, int>();
            NewDict["x"] = xS;
            NewDict["y"] = yS;
            return NewDict;
        }

        public Dictionary<string, int> ConvertIndexToXY(int Thisndex)
        {
            int yS = (int)Math.Floor((double)Thisndex / 10);
            int xS = Thisndex - (yS * 10);

            Dictionary<string, int> NewDict = new Dictionary<string, int>();
            NewDict["x"] = xS;
            NewDict["y"] = yS;
            return NewDict;
        }

        public Dictionary<string, int> ConvertIndexToCubeXY(int Thisndex)
        {
            int yS = (int)Math.Floor((double)Thisndex / 3);
            int xS = Thisndex - (yS * 3);

            Dictionary<string, int> NewDict = new Dictionary<string, int>();
            NewDict["x"] = xS;
            NewDict["y"] = yS;
            return NewDict;
        }

    }
}
