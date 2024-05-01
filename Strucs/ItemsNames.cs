using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemsNames
{
    public string setNamefromId(int uniqueOrSetId)
    {
        switch (uniqueOrSetId)
        {
            case 0:
                return "Civerb's Ward";
            case 1:
                return "Civerb's Icon";
            case 2:
                return "Civerb's Cudgel";
            case 3:
                return "Hsarus' Iron Heel";
            case 4:
                return "Hsarus' Iron Fist";
            case 5:
                return "Hsarus' Iron Stay";
            case 6:
                return "Cleglaw's Tooth";
            case 7:
                return "Cleglaw's Claw";
            case 8:
                return "Cleglaw's Pincers";
            case 9:
                return "Iratha's Collar";
            case 10:
                return "Iratha's Cuff";
            case 11:
                return "Iratha's Coil";
            case 12:
                return "Iratha's Cord";
            case 13:
                return "Isenhart's Lightbrand";
            case 14:
                return "Isenhart's Parry";
            case 15:
                return "Isenhart's Case";
            case 16:
                return "Isenhart's Horns";
            case 17:
                return "Vidala's Barb";
            case 18:
                return "Vidala's Fetlock";
            case 19:
                return "Vidala's Ambush";
            case 20:
                return "Vidala's Snare";
            case 21:
                return "Milabrega's Orb";
            case 22:
                return "Milabrega's Rod";
            case 23:
                return "Milabrega's Diadem";
            case 24:
                return "Milabrega's Robe";
            case 25:
                return "Cathan's Rule";
            case 26:
                return "Cathan's Mesh";
            case 27:
                return "Cathan's Visage";
            case 28:
                return "Cathan's Sigil";
            case 29:
                return "Cathan's Seal";
            case 30:
                return "Tancred's Crowbill";
            case 31:
                return "Tancred's Spine";
            case 32:
                return "Tancred's Hobnails";
            case 33:
                return "Tancred's Weird";
            case 34:
                return "Tancred's Skull";
            case 35:
                return "Sigon's Gage";
            case 36:
                return "Sigon's Visor";
            case 37:
                return "Sigon's Shelter";
            case 38:
                return "Sigon's Sabot";
            case 39:
                return "Sigon's Wrap";
            case 40:
                return "Sigon's Guard";
            case 41:
                return "Infernal Cranium";
            case 42:
                return "Infernal Torch";
            case 43:
                return "Infernal Sign";
            case 44:
                return "Berserker's Headgear";
            case 45:
                return "Berserker's Hauberk";
            case 46:
                return "Berserker's Hatchet";
            case 47:
                return "Death's Hand";
            case 48:
                return "Death's Guard";
            case 49:
                return "Death's Touch";
            case 50:
                return "Angelic Sickle";
            case 51:
                return "Angelic Mantle";
            case 52:
                return "Angelic Halo";
            case 53:
                return "Angelic Wings";
            case 54:
                return "Arctic Horn";
            case 55:
                return "Arctic Furs";
            case 56:
                return "Arctic Binding";
            case 57:
                return "Arctic Mitts";
            case 58:
                return "Arcanna's Sign";
            case 59:
                return "Arcanna's Deathwand";
            case 60:
                return "Arcanna's Head";
            case 61:
                return "Arcanna's Flesh";
            case 62:
                return "Natalya's Totem";
            case 63:
                return "Natalya's Mark";
            case 64:
                return "Natalya's Shadow";
            case 65:
                return "Natalya's Soul";
            case 66:
                return "Aldur's Stony Gaze";
            case 67:
                return "Aldur's Deception";
            case 68:
                return "Aldur's Gauntlet";
            case 69:
                return "Aldur's Advance";
            case 70:
                return "Immortal King's Will";
            case 71:
                return "Immortal King's Soul Cage";
            case 72:
                return "Immortal King's Detail";
            case 73:
                return "Immortal King's Forge";
            case 74:
                return "Immortal King's Pillar";
            case 75:
                return "Immortal King's Stone Crusher";
            case 76:
                return "Tal Rasha's Fire-Spun Cloth";
            case 77:
                return "Tal Rasha's Adjudication";
            case 78:
                return "Tal Rasha's Lidless Eye";
            case 79:
                return "Tal Rasha's Howling Wind";
            case 80:
                return "Tal Rasha's Horadric Crest";
            case 81:
                return "Griswold's Valor";
            case 82:
                return "Griswold's Heart";
            case 83:
                return "Griswolds's Redemption";
            case 84:
                return "Griswold's Honor";
            case 85:
                return "Trang-Oul's Guise";
            case 86:
                return "Trang-Oul's Scales";
            case 87:
                return "Trang-Oul's Wing";
            case 88:
                return "Trang-Oul's Claws";
            case 89:
                return "Trang-Oul's Girth";
            case 90:
                return "M'avina's True Sight";
            case 91:
                return "M'avina's Embrace";
            case 92:
                return "M'avina's Icy Clutch";
            case 93:
                return "M'avina's Tenet";
            case 94:
                return "M'avina's Caster";
            case 95:
                return "Telling of Beads";
            case 96:
                return "Laying of Hands";
            case 97:
                return "Rite of Passage";
            case 98:
                return "Spiritual Custodian";
            case 99:
                return "Credendum";
            case 100:
                return "Dangoon's Teaching";
            case 101:
                return "Heaven's Taebaek";
            case 102:
                return "Haemosu's Adament";
            case 103:
                return "Ondal's Almighty";
            case 104:
                return "Guillaume's Face";
            case 105:
                return "Wilhelm's Pride";
            case 106:
                return "Magnus' Skin";
            case 107:
                return "Wihtstan's Guard";
            case 108:
                return "Hwanin's Splendor";
            case 109:
                return "Hwanin's Refuge";
            case 110:
                return "Hwanin's Seal";
            case 111:
                return "Hwanin's Justice";
            case 112:
                return "Sazabi's Cobalt Redeemer";
            case 113:
                return "Sazabi's Ghost Liberator";
            case 114:
                return "Sazabi's Mental Sheath";
            case 115:
                return "Bul-Kathos' Sacred Charge";
            case 116:
                return "Bul-Kathos' Tribal Guardian";
            case 117:
                return "Cow King's Horns";
            case 118:
                return "Cow King's Hide";
            case 119:
                return "Cow King's Hoofs";
            case 120:
                return "Naj's Puzzler";
            case 121:
                return "Naj's Light Plate";
            case 122:
                return "Naj's Circlet";
            case 123:
                return "McAuley's Paragon";
            case 124:
                return "McAuley's Riprap";
            case 125:
                return "McAuley's Taboo";
            case 126:
                return "McAuley's Superstition";
            default:
                return "";
        }
    }

    string uniqueNamefromId(int uniqueOrSetId)
    {
        switch (uniqueOrSetId)
        {
            case 0:
                return "The Gnasher";
            case 1:
                return "Deathspade";
            case 2:
                return "Bladebone";
            case 3:
                return "Mindrend";
            case 4:
                return "Rakescar";
            case 5:
                return "Fechmars Axe";
            case 6:
                return "Goreshovel";
            case 7:
                return "The Chieftan";
            case 8:
                return "Brainhew";
            case 9:
                return "The Humongous";
            case 10:
                return "Iros Torch";
            case 11:
                return "Maelstromwrath";
            case 12:
                return "Gravenspine";
            case 13:
                return "Umes Lament";
            case 14:
                return "Felloak";
            case 15:
                return "Knell Striker";
            case 16:
                return "Rusthandle";
            case 17:
                return "Stormeye";
            case 18:
                return "Stoutnail";
            case 19:
                return "Crushflange";
            case 20:
                return "Bloodrise";
            case 21:
                return "The Generals Tan Do Li Ga";
            case 22:
                return "Ironstone";
            case 23:
                return "Bonesob";
            case 24:
                return "Steeldriver";
            case 25:
                return "Rixots Keen";
            case 26:
                return "Blood Crescent";
            case 27:
                return "Krintizs Skewer";
            case 28:
                return "Gleamscythe";
            case 29:
                return "Azurewrath";
            case 30:
                return "Griswolds Edge";
            case 31:
                return "Hellplague";
            case 32:
                return "Culwens Point";
            case 33:
                return "Shadowfang";
            case 34:
                return "Soulflay";
            case 35:
                return "Kinemils Awl";
            case 36:
                return "Blacktongue";
            case 37:
                return "Ripsaw";
            case 38:
                return "The Patriarch";
            case 39:
                return "Gull";
            case 40:
                return "The Diggler";
            case 41:
                return "The Jade Tan Do";
            case 42:
                return "Irices Shard";
            case 43:
                return "The Dragon Chang";
            case 44:
                return "Razortine";
            case 45:
                return "Bloodthief";
            case 46:
                return "Lance of Yaggai";
            case 47:
                return "The Tannr Gorerod";
            case 48:
                return "Dimoaks Hew";
            case 49:
                return "Steelgoad";
            case 50:
                return "Soul Harvest";
            case 51:
                return "The Battlebranch";
            case 52:
                return "Woestave";
            case 53:
                return "The Grim Reaper";
            case 54:
                return "Bane Ash";
            case 55:
                return "Serpent Lord";
            case 56:
                return "Lazarus Spire";
            case 57:
                return "The Salamander";
            case 58:
                return "The Iron Jang Bong";
            case 59:
                return "Pluckeye";
            case 60:
                return "Witherstring";
            case 61:
                return "Rimeraven";
            case 62:
                return "Piercerib";
            case 63:
                return "Pullspite";
            case 64:
                return "Wizendraw";
            case 65:
                return "Hellclap";
            case 66:
                return "Blastbark";
            case 67:
                return "Leadcrow";
            case 68:
                return "Ichorsting";
            case 69:
                return "Hellcast";
            case 70:
                return "Doomspittle";
            case 71:
                return "War Bonnet";
            case 72:
                return "Tarnhelm";
            case 73:
                return "Coif of Glory";
            case 74:
                return "Duskdeep";
            case 75:
                return "Wormskull";
            case 76:
                return "Howltusk";
            case 77:
                return "Undead Crown";
            case 78:
                return "The Face of Horror";
            case 79:
                return "Greyform";
            case 80:
                return "Blinkbats Form";
            case 81:
                return "The Centurion";
            case 82:
                return "Twitchthroe";
            case 83:
                return "Darkglow";
            case 84:
                return "Hawkmail";
            case 85:
                return "Sparking Mail";
            case 86:
                return "Venomsward";
            case 87:
                return "Iceblink";
            case 88:
                return "Boneflesh";
            case 89:
                return "Rockfleece";
            case 90:
                return "Rattlecage";
            case 91:
                return "Goldskin";
            case 92:
                return "Victors Silk";
            case 93:
                return "Heavenly Garb";
            case 94:
                return "Pelta Lunata";
            case 95:
                return "Umbral Disk";
            case 96:
                return "Stormguild";
            case 97:
                return "Wall of the Eyeless";
            case 98:
                return "Swordback Hold";
            case 99:
                return "Steelclash";
            case 100:
                return "Bverrit Keep";
            case 101:
                return "The Ward";
            case 102:
                return "The Hand of Broc";
            case 103:
                return "Bloodfist";
            case 104:
                return "Chance Guards";
            case 105:
                return "Magefist";
            case 106:
                return "Frostburn";
            case 107:
                return "Hotspur";
            case 108:
                return "Gorefoot";
            case 109:
                return "Treads of Cthon";
            case 110:
                return "Goblin Toe";
            case 111:
                return "Tearhaunch";
            case 112:
                return "Lenyms Cord";
            case 113:
                return "Snakecord";
            case 114:
                return "Nightsmoke";
            case 115:
                return "Goldwrap";
            case 116:
                return "Bladebuckle";
            case 117:
                return "Nokozan Relic";
            case 118:
                return "The Eye of Etlich";
            case 119:
                return "The Mahim-Oak Curio";
            case 120:
                return "Nagelring";
            case 121:
                return "Manald Heal";
            case 122:
                return "The Stone of Jordan";
            case 123:
                return "Amulet of the Viper";
            case 124:
                return "Staff of Kings";
            case 125:
                return "Horadric Staff";
            case 126:
                return "Hell Forge Hammer";
            case 127:
                return "KhalimFlail";
            case 128:
                return "SuperKhalimFlail";
            case 129:
                return "Coldkill";
            case 130:
                return "Butcher's Pupil";
            case 131:
                return "Islestrike";
            case 132:
                return "Pompe's Wrath";
            case 133:
                return "Guardian Naga";
            case 134:
                return "Warlord's Trust";
            case 135:
                return "Spellsteel";
            case 136:
                return "Stormrider";
            case 137:
                return "Boneslayer Blade";
            case 138:
                return "The Minataur";
            case 139:
                return "Suicide Branch";
            case 140:
                return "Carin Shard";
            case 141:
                return "Arm of King Leoric";
            case 142:
                return "Blackhand Key";
            case 143:
                return "Dark Clan Crusher";
            case 144:
                return "Zakarum's Hand";
            case 145:
                return "The Fetid Sprinkler";
            case 146:
                return "Hand of Blessed Light";
            case 147:
                return "Fleshrender";
            case 148:
                return "Sureshrill Frost";
            case 149:
                return "Moonfall";
            case 150:
                return "Baezil's Vortex";
            case 151:
                return "Earthshaker";
            case 152:
                return "Bloodtree Stump";
            case 153:
                return "The Gavel of Pain";
            case 154:
                return "Bloodletter";
            case 155:
                return "Coldsteel Eye";
            case 156:
                return "Hexfire";
            case 157:
                return "Blade of Ali Baba";
            case 158:
                return "Ginther's Rift";
            case 159:
                return "Headstriker";
            case 160:
                return "Plague Bearer";
            case 161:
                return "The Atlantian";
            case 162:
                return "Crainte Vomir";
            case 163:
                return "Bing Sz Wang";
            case 164:
                return "The Vile Husk";
            case 165:
                return "Cloudcrack";
            case 166:
                return "Todesfaelle Flamme";
            case 167:
                return "Swordguard";
            case 168:
                return "Spineripper";
            case 169:
                return "Heart Carver";
            case 170:
                return "Blackbog's Sharp";
            case 171:
                return "Stormspike";
            case 172:
                return "The Impaler";
            case 173:
                return "Kelpie Snare";
            case 174:
                return "Soulfeast Tine";
            case 175:
                return "Hone Sundan";
            case 176:
                return "Spire of Honor";
            case 177:
                return "The Meat Scraper";
            case 178:
                return "Blackleach Blade";
            case 179:
                return "Athena's Wrath";
            case 180:
                return "Pierre Tombale Couant";
            case 181:
                return "Husoldal Evo";
            case 182:
                return "Grim's Burning Dead";
            case 183:
                return "Razorswitch";
            case 184:
                return "Ribcracker";
            case 185:
                return "Chromatic Ire";
            case 186:
                return "Warpspear";
            case 187:
                return "Skullcollector";
            case 188:
                return "Skystrike";
            case 189:
                return "Riphook";
            case 190:
                return "Kuko Shakaku";
            case 191:
                return "Endlesshail";
            case 192:
                return "Whichwild String";
            case 193:
                return "Cliffkiller";
            case 194:
                return "Magewrath";
            case 195:
                return "Godstrike Arch";
            case 196:
                return "Langer Briser";
            case 197:
                return "Pus Spiter";
            case 198:
                return "Buriza-Do Kyanon";
            case 199:
                return "Demon Machine";
            case 201:
                return "Peasent Crown";
            case 202:
                return "Rockstopper";
            case 203:
                return "Stealskull";
            case 204:
                return "Darksight Helm";
            case 205:
                return "Valkiry Wing";
            case 206:
                return "Crown of Thieves";
            case 207:
                return "Blackhorn's Face";
            case 208:
                return "Vampiregaze";
            case 209:
                return "The Spirit Shroud";
            case 210:
                return "Skin of the Vipermagi";
            case 211:
                return "Skin of the Flayerd One";
            case 212:
                return "Ironpelt";
            case 213:
                return "Spiritforge";
            case 214:
                return "Crow Caw";
            case 215:
                return "Shaftstop";
            case 216:
                return "Duriel's Shell";
            case 217:
                return "Skullder's Ire";
            case 218:
                return "Guardian Angel";
            case 219:
                return "Toothrow";
            case 220:
                return "Atma's Wail";
            case 221:
                return "Black Hades";
            case 222:
                return "Corpsemourn";
            case 223:
                return "Que-Hegan's Wisdon";
            case 224:
                return "Visceratuant";
            case 225:
                return "Mosers Blessed Circle";
            case 226:
                return "Stormchaser";
            case 227:
                return "Tiamat's Rebuke";
            case 228:
                return "Kerke's Sanctuary";
            case 229:
                return "Radimant's Sphere";
            case 230:
                return "Lidless Wall";
            case 231:
                return "Lance Guard";
            case 232:
                return "Venom Grip";
            case 233:
                return "Gravepalm";
            case 234:
                return "Ghoulhide";
            case 235:
                return "Lavagout";
            case 236:
                return "Hellmouth";
            case 237:
                return "Infernostride";
            case 238:
                return "Waterwalk";
            case 239:
                return "Silkweave";
            case 240:
                return "Wartraveler";
            case 241:
                return "Gorerider";
            case 242:
                return "String of Ears";
            case 243:
                return "Razortail";
            case 244:
                return "Gloomstrap";
            case 245:
                return "Snowclash";
            case 246:
                return "Thudergod's Vigor";
            case 248:
                return "Harlequin Crest";
            case 249:
                return "Veil of Steel";
            case 250:
                return "The Gladiator's Bane";
            case 251:
                return "Arkaine's Valor";
            case 252:
                return "Blackoak Shield";
            case 253:
                return "Stormshield";
            case 254:
                return "Hellslayer";
            case 255:
                return "Messerschmidt's Reaver";
            case 256:
                return "Baranar's Star";
            case 257:
                return "Schaefer's Hammer";
            case 258:
                return "The Cranium Basher";
            case 259:
                return "Lightsabre";
            case 260:
                return "Doombringer";
            case 261:
                return "The Grandfather";
            case 262:
                return "Wizardspike";
            case 263:
                return "Constricting Ring";
            case 264:
                return "Stormspire";
            case 265:
                return "Eaglehorn";
            case 266:
                return "Windforce";
            case 268:
                return "Bul Katho's Wedding Band";
            case 269:
                return "The Cat's Eye";
            case 270:
                return "The Rising Sun";
            case 271:
                return "Crescent Moon";
            case 272:
                return "Mara's Kaleidoscope";
            case 273:
                return "Atma's Scarab";
            case 274:
                return "Dwarf Star";
            case 275:
                return "Raven Frost";
            case 276:
                return "Highlord's Wrath";
            case 277:
                return "Saracen's Chance";
            case 279:
                return "Arreat's Face";
            case 280:
                return "Homunculus";
            case 281:
                return "Titan's Revenge";
            case 282:
                return "Lycander's Aim";
            case 283:
                return "Lycander's Flank";
            case 284:
                return "The Oculus";
            case 285:
                return "Herald of Zakarum";
            case 286:
                return "Cutthroat1";
            case 287:
                return "Jalal's Mane";
            case 288:
                return "The Scalper";
            case 289:
                return "Bloodmoon";
            case 290:
                return "Djinnslayer";
            case 291:
                return "Deathbit";
            case 292:
                return "Warshrike";
            case 293:
                return "Gutsiphon";
            case 294:
                return "Razoredge";
            case 295:
                return "Gore Ripper";
            case 296:
                return "Demonlimb";
            case 297:
                return "Steelshade";
            case 298:
                return "Tomb Reaver";
            case 299:
                return "Deaths's Web";
            case 300:
                return "Nature's Peace";
            case 301:
                return "Azurewrath";
            case 302:
                return "Seraph's Hymn";
            case 303:
                return "Zakarum's Salvation";
            case 304:
                return "Fleshripper";
            case 305:
                return "Odium";
            case 306:
                return "Horizon's Tornado";
            case 307:
                return "Stone Crusher";
            case 308:
                return "Jadetalon";
            case 309:
                return "Shadowdancer";
            case 310:
                return "Cerebus";
            case 311:
                return "Tyrael's Might";
            case 312:
                return "Souldrain";
            case 313:
                return "Runemaster";
            case 314:
                return "Deathcleaver";
            case 315:
                return "Executioner's Justice";
            case 316:
                return "Stoneraven";
            case 317:
                return "Leviathan";
            case 318:
                return "Larzuk's Champion";
            case 319:
                return "Wisp";
            case 320:
                return "Gargoyle's Bite";
            case 321:
                return "Lacerator";
            case 322:
                return "Mang Song's Lesson";
            case 323:
                return "Viperfork";
            case 324:
                return "Ethereal Edge";
            case 325:
                return "Demonhorn's Edge";
            case 326:
                return "The Reaper's Toll";
            case 327:
                return "Spiritkeeper";
            case 328:
                return "Hellrack";
            case 329:
                return "Alma Negra";
            case 330:
                return "Darkforge Spawn";
            case 331:
                return "Widowmaker";
            case 332:
                return "Bloodraven's Charge";
            case 333:
                return "Ghostflame";
            case 334:
                return "Shadowkiller";
            case 335:
                return "Gimmershred";
            case 336:
                return "Griffon's Eye";
            case 337:
                return "Windhammer";
            case 338:
                return "Thunderstroke";
            case 339:
                return "Giantmaimer";
            case 340:
                return "Demon's Arch";
            case 341:
                return "Boneflame";
            case 342:
                return "Steelpillar";
            case 343:
                return "Nightwing's Veil";
            case 344:
                return "Crown of Ages";
            case 345:
                return "Andariel's Visage";
            case 346:
                return "Darkfear";
            case 347:
                return "Dragonscale";
            case 348:
                return "Steel Carapice";
            case 349:
                return "Medusa's Gaze";
            case 350:
                return "Ravenlore";
            case 351:
                return "Boneshade";
            case 352:
                return "Nethercrow";
            case 353:
                return "Flamebellow";
            case 354:
                return "Fathom";
            case 355:
                return "Wolfhowl";
            case 356:
                return "Spirit Ward";
            case 357:
                return "Kira's Guardian";
            case 358:
                return "Ormus' Robes";
            case 359:
                return "Gheed's Fortune";
            case 360:
                return "Stormlash";
            case 361:
                return "Halaberd's Reign";
            case 362:
                return "Warriv's Warder";
            case 363:
                return "Spike Thorn";
            case 364:
                return "Dracul's Grasp";
            case 365:
                return "Frostwind";
            case 366:
                return "Templar's Might";
            case 367:
                return "Eschuta's temper";
            case 368:
                return "Firelizard's Talons";
            case 369:
                return "Sandstorm Trek";
            case 370:
                return "Marrowwalk";
            case 371:
                return "Heaven's Light";
            case 372:
                return "Merman's Speed";
            case 373:
                return "Arachnid Mesh";
            case 374:
                return "Nosferatu's Coil";
            case 375:
                return "Metalgrid";
            case 376:
                return "Verdugo's Hearty Cord";
            case 377:
                return "Sigurd's Staunch";
            case 378:
                return "Carrion Wind";
            case 379:
                return "Giantskull";
            case 380:
                return "Ironward";
            case 381:
                return "Annihilus";
            case 382:
                return "Arioc's Needle";
            case 383:
                return "Cranebeak";
            case 384:
                return "Nord's Tenderizer";
            case 385:
                return "Earthshifter";
            case 386:
                return "Wraithflight";
            case 387:
                return "Bonehew";
            case 388:
                return "Ondal's Wisdom";
            case 389:
                return "The Reedeemer";
            case 390:
                return "Headhunter's Glory";
            case 391:
                return "Steelrend";
            case 392:
                return "Rainbow Facet";
            case 393:
                return "Rainbow Facet";
            case 394:
                return "Rainbow Facet";
            case 395:
                return "Rainbow Facet";
            case 396:
                return "Rainbow Facet";
            case 397:
                return "Rainbow Facet";
            case 398:
                return "Rainbow Facet";
            case 399:
                return "Rainbow Facet";
            case 400:
                return "Hellfire Torch";
            case 401:
                return "Cold Rupture";
            case 402:
                return "Flame Rift";
            case 403:
                return "Crack of the Heavens";
            case 404:
                return "Rotting Fissure";
            case 405:
                return "Bone Break";
            case 406:
                return "Black Cleft";
            default:
                return "";
        }
    }

    string getLocalizedName(int txtFileNo)
    {
        switch (txtFileNo)
        {
            case 0:
                return "hax";
            case 1:
                return "axe";
            case 2:
                return "2ax";
            case 3:
                return "mpi";
            case 4:
                return "wax";
            case 5:
                return "lax";
            case 6:
                return "bax";
            case 7:
                return "btx";
            case 8:
                return "gax";
            case 9:
                return "gix";
            case 10:
                return "wnd";
            case 11:
                return "ywn";
            case 12:
                return "bwn";
            case 13:
                return "gwn";
            case 14:
                return "clb";
            case 15:
                return "scp";
            case 16:
                return "gsc";
            case 17:
                return "wsp";
            case 18:
                return "spc";
            case 19:
                return "mac";
            case 20:
                return "mst";
            case 21:
                return "fla";
            case 22:
                return "whm";
            case 23:
                return "mau";
            case 24:
                return "gma";
            case 25:
                return "ssd";
            case 26:
                return "scm";
            case 27:
                return "sbr";
            case 28:
                return "flc";
            case 29:
                return "crs";
            case 30:
                return "bsd";
            case 31:
                return "lsd";
            case 32:
                return "wsd";
            case 33:
                return "2hs";
            case 34:
                return "clm";
            case 35:
                return "gis";
            case 36:
                return "bsw";
            case 37:
                return "flb";
            case 38:
                return "gsd";
            case 39:
                return "dgr";
            case 40:
                return "dir";
            case 41:
                return "kri";
            case 42:
                return "bld";
            case 43:
                return "tkf";
            case 44:
                return "tax";
            case 45:
                return "bkf";
            case 46:
                return "bal";
            case 47:
                return "jav";
            case 48:
                return "pil";
            case 49:
                return "ssp";
            case 50:
                return "glv";
            case 51:
                return "tsp";
            case 52:
                return "spr";
            case 53:
                return "tri";
            case 54:
                return "brn";
            case 55:
                return "spt";
            case 56:
                return "pik";
            case 57:
                return "bar";
            case 58:
                return "vou";
            case 59:
                return "scy";
            case 60:
                return "pax";
            case 61:
                return "hal";
            case 62:
                return "wsc";
            case 63:
                return "sst";
            case 64:
                return "lst";
            case 65:
                return "cst";
            case 66:
                return "bst";
            case 67:
                return "wst";
            case 68:
                return "sbw";
            case 69:
                return "hbw";
            case 70:
                return "lbw";
            case 71:
                return "cbw";
            case 72:
                return "sbb";
            case 73:
                return "lbb";
            case 74:
                return "swb";
            case 75:
                return "lwb";
            case 76:
                return "lxb";
            case 77:
                return "mxb";
            case 78:
                return "hxb";
            case 79:
                return "rxb";
            case 80:
                return "gps";
            case 81:
                return "ops";
            case 82:
                return "gpm";
            case 83:
                return "opm";
            case 84:
                return "gpl";
            case 85:
                return "opl";
            case 86:
                return "d33";
            case 87:
                return "g33";
            case 88:
                return "leg";
            case 89:
                return "hdm";
            case 90:
                return "hfh";
            case 91:
                return "hst";
            case 92:
                return "msf";
            case 93:
                return "9ha";
            case 94:
                return "9ax";
            case 95:
                return "92a";
            case 96:
                return "9mp";
            case 97:
                return "9wa";
            case 98:
                return "9la";
            case 99:
                return "9ba";
            case 100:
                return "9bt";
            case 101:
                return "9ga";
            case 102:
                return "9gi";
            case 103:
                return "9wn";
            case 104:
                return "9yw";
            case 105:
                return "9bw";
            case 106:
                return "9gw";
            case 107:
                return "9cl";
            case 108:
                return "9sc";
            case 109:
                return "9qs";
            case 110:
                return "9ws";
            case 111:
                return "9sp";
            case 112:
                return "9ma";
            case 113:
                return "9mt";
            case 114:
                return "9fl";
            case 115:
                return "9wh";
            case 116:
                return "9m9";
            case 117:
                return "9gm";
            case 118:
                return "9ss";
            case 119:
                return "9sm";
            case 120:
                return "9sb";
            case 121:
                return "9fc";
            case 122:
                return "9cr";
            case 123:
                return "9bs";
            case 124:
                return "9ls";
            case 125:
                return "9wd";
            case 126:
                return "92h";
            case 127:
                return "9cm";
            case 128:
                return "9gs";
            case 129:
                return "9b9";
            case 130:
                return "9fb";
            case 131:
                return "9gd";
            case 132:
                return "9dg";
            case 133:
                return "9di";
            case 134:
                return "9kr";
            case 135:
                return "9bl";
            case 136:
                return "9tk";
            case 137:
                return "9ta";
            case 138:
                return "9bk";
            case 139:
                return "9b8";
            case 140:
                return "9ja";
            case 141:
                return "9pi";
            case 142:
                return "9s9";
            case 143:
                return "9gl";
            case 144:
                return "9ts";
            case 145:
                return "9sr";
            case 146:
                return "9tr";
            case 147:
                return "9br";
            case 148:
                return "9st";
            case 149:
                return "9p9";
            case 150:
                return "9b7";
            case 151:
                return "9vo";
            case 152:
                return "9s8";
            case 153:
                return "9pa";
            case 154:
                return "9h9";
            case 155:
                return "9wc";
            case 156:
                return "8ss";
            case 157:
                return "8ls";
            case 158:
                return "8cs";
            case 159:
                return "8bs";
            case 160:
                return "8ws";
            case 161:
                return "8sb";
            case 162:
                return "8hb";
            case 163:
                return "8lb";
            case 164:
                return "8cb";
            case 165:
                return "8s8";
            case 166:
                return "8l8";
            case 167:
                return "8sw";
            case 168:
                return "8lw";
            case 169:
                return "8lx";
            case 170:
                return "8mx";
            case 171:
                return "8hx";
            case 172:
                return "8rx";
            case 173:
                return "qf1";
            case 174:
                return "qf2";
            case 175:
                return "ktr";
            case 176:
                return "wrb";
            case 177:
                return "axf";
            case 178:
                return "ces";
            case 179:
                return "clw";
            case 180:
                return "btl";
            case 181:
                return "skr";
            case 182:
                return "9ar";
            case 183:
                return "9wb";
            case 184:
                return "9xf";
            case 185:
                return "9cs";
            case 186:
                return "9lw";
            case 187:
                return "9tw";
            case 188:
                return "9qr";
            case 189:
                return "7ar";
            case 190:
                return "7wb";
            case 191:
                return "7xf";
            case 192:
                return "7cs";
            case 193:
                return "7lw";
            case 194:
                return "7tw";
            case 195:
                return "7qr";
            case 196:
                return "7ha";
            case 197:
                return "7ax";
            case 198:
                return "72a";
            case 199:
                return "7mp";
            case 200:
                return "7wa";
            case 201:
                return "7la";
            case 202:
                return "7ba";
            case 203:
                return "7bt";
            case 204:
                return "7ga";
            case 205:
                return "7gi";
            case 206:
                return "7wn";
            case 207:
                return "7yw";
            case 208:
                return "7bw";
            case 209:
                return "7gw";
            case 210:
                return "7cl";
            case 211:
                return "7sc";
            case 212:
                return "7qs";
            case 213:
                return "7ws";
            case 214:
                return "7sp";
            case 215:
                return "7ma";
            case 216:
                return "7mt";
            case 217:
                return "7fl";
            case 218:
                return "7wh";
            case 219:
                return "7m7";
            case 220:
                return "7gm";
            case 221:
                return "7ss";
            case 222:
                return "7sm";
            case 223:
                return "7sb";
            case 224:
                return "7fc";
            case 225:
                return "7cr";
            case 226:
                return "7bs";
            case 227:
                return "7ls";
            case 228:
                return "7wd";
            case 229:
                return "72h";
            case 230:
                return "7cm";
            case 231:
                return "7gs";
            case 232:
                return "7b7";
            case 233:
                return "7fb";
            case 234:
                return "7gd";
            case 235:
                return "7dg";
            case 236:
                return "7di";
            case 237:
                return "7kr";
            case 238:
                return "7bl";
            case 239:
                return "7tk";
            case 240:
                return "7ta";
            case 241:
                return "7bk";
            case 242:
                return "7b8";
            case 243:
                return "7ja";
            case 244:
                return "7pi";
            case 245:
                return "7s7";
            case 246:
                return "7gl";
            case 247:
                return "7ts";
            case 248:
                return "7sr";
            case 249:
                return "7tr";
            case 250:
                return "7br";
            case 251:
                return "7st";
            case 252:
                return "7p7";
            case 253:
                return "7o7";
            case 254:
                return "7vo";
            case 255:
                return "7s8";
            case 256:
                return "7pa";
            case 257:
                return "7h7";
            case 258:
                return "7wc";
            case 259:
                return "6ss";
            case 260:
                return "6ls";
            case 261:
                return "6cs";
            case 262:
                return "6bs";
            case 263:
                return "6ws";
            case 264:
                return "6sb";
            case 265:
                return "6hb";
            case 266:
                return "6lb";
            case 267:
                return "6cb";
            case 268:
                return "6s7";
            case 269:
                return "6l7";
            case 270:
                return "6sw";
            case 271:
                return "6lw";
            case 272:
                return "6lx";
            case 273:
                return "6mx";
            case 274:
                return "6hx";
            case 275:
                return "6rx";
            case 276:
                return "ob1";
            case 277:
                return "ob2";
            case 278:
                return "ob3";
            case 279:
                return "ob4";
            case 280:
                return "ob5";
            case 281:
                return "am1";
            case 282:
                return "am2";
            case 283:
                return "am3";
            case 284:
                return "am4";
            case 285:
                return "am5";
            case 286:
                return "ob6";
            case 287:
                return "ob7";
            case 288:
                return "ob8";
            case 289:
                return "ob9";
            case 290:
                return "oba";
            case 291:
                return "am6";
            case 292:
                return "am7";
            case 293:
                return "am8";
            case 294:
                return "am9";
            case 295:
                return "ama";
            case 296:
                return "obb";
            case 297:
                return "obc";
            case 298:
                return "obd";
            case 299:
                return "obe";
            case 300:
                return "obf";
            case 301:
                return "amb";
            case 302:
                return "amc";
            case 303:
                return "amd";
            case 304:
                return "ame";
            case 305:
                return "amf";
            case 306:
                return "cap";
            case 307:
                return "skp";
            case 308:
                return "hlm";
            case 309:
                return "fhl";
            case 310:
                return "ghm";
            case 311:
                return "crn";
            case 312:
                return "msk";
            case 313:
                return "qui";
            case 314:
                return "lea";
            case 315:
                return "hla";
            case 316:
                return "stu";
            case 317:
                return "rng";
            case 318:
                return "scl";
            case 319:
                return "chn";
            case 320:
                return "brs";
            case 321:
                return "spl";
            case 322:
                return "plt";
            case 323:
                return "fld";
            case 324:
                return "gth";
            case 325:
                return "ful";
            case 326:
                return "aar";
            case 327:
                return "ltp";
            case 328:
                return "buc";
            case 329:
                return "sml";
            case 330:
                return "lrg";
            case 331:
                return "kit";
            case 332:
                return "tow";
            case 333:
                return "gts";
            case 334:
                return "lgl";
            case 335:
                return "vgl";
            case 336:
                return "mgl";
            case 337:
                return "tgl";
            case 338:
                return "hgl";
            case 339:
                return "lbt";
            case 340:
                return "vbt";
            case 341:
                return "mbt";
            case 342:
                return "tbt";
            case 343:
                return "hbt";
            case 344:
                return "lbl";
            case 345:
                return "vbl";
            case 346:
                return "mbl";
            case 347:
                return "tbl";
            case 348:
                return "hbl";
            case 349:
                return "bhm";
            case 350:
                return "bsh";
            case 351:
                return "spk";
            case 352:
                return "xap";
            case 353:
                return "xkp";
            case 354:
                return "xlm";
            case 355:
                return "xhl";
            case 356:
                return "xhm";
            case 357:
                return "xrn";
            case 358:
                return "xsk";
            case 359:
                return "xui";
            case 360:
                return "xea";
            case 361:
                return "xla";
            case 362:
                return "xtu";
            case 363:
                return "xng";
            case 364:
                return "xcl";
            case 365:
                return "xhn";
            case 366:
                return "xrs";
            case 367:
                return "xpl";
            case 368:
                return "xlt";
            case 369:
                return "xld";
            case 370:
                return "xth";
            case 371:
                return "xul";
            case 372:
                return "xar";
            case 373:
                return "xtp";
            case 374:
                return "xuc";
            case 375:
                return "xml";
            case 376:
                return "xrg";
            case 377:
                return "xit";
            case 378:
                return "xow";
            case 379:
                return "xts";
            case 380:
                return "xlg";
            case 381:
                return "xvg";
            case 382:
                return "xmg";
            case 383:
                return "xtg";
            case 384:
                return "xhg";
            case 385:
                return "xlb";
            case 386:
                return "xvb";
            case 387:
                return "xmb";
            case 388:
                return "xtb";
            case 389:
                return "xhb";
            case 390:
                return "zlb";
            case 391:
                return "zvb";
            case 392:
                return "zmb";
            case 393:
                return "ztb";
            case 394:
                return "zhb";
            case 395:
                return "xh9";
            case 396:
                return "xsh";
            case 397:
                return "xpk";
            case 398:
                return "dr1";
            case 399:
                return "dr2";
            case 400:
                return "dr3";
            case 401:
                return "dr4";
            case 402:
                return "dr5";
            case 403:
                return "ba1";
            case 404:
                return "ba2";
            case 405:
                return "ba3";
            case 406:
                return "ba4";
            case 407:
                return "ba5";
            case 408:
                return "pa1";
            case 409:
                return "pa2";
            case 410:
                return "pa3";
            case 411:
                return "pa4";
            case 412:
                return "pa5";
            case 413:
                return "ne1";
            case 414:
                return "ne2";
            case 415:
                return "ne3";
            case 416:
                return "ne4";
            case 417:
                return "ne5";
            case 418:
                return "ci0";
            case 419:
                return "ci1";
            case 420:
                return "ci2";
            case 421:
                return "ci3";
            case 422:
                return "uap";
            case 423:
                return "ukp";
            case 424:
                return "ulm";
            case 425:
                return "uhl";
            case 426:
                return "uhm";
            case 427:
                return "urn";
            case 428:
                return "usk";
            case 429:
                return "uui";
            case 430:
                return "uea";
            case 431:
                return "ula";
            case 432:
                return "utu";
            case 433:
                return "ung";
            case 434:
                return "ucl";
            case 435:
                return "uhn";
            case 436:
                return "urs";
            case 437:
                return "upl";
            case 438:
                return "ult";
            case 439:
                return "uld";
            case 440:
                return "uth";
            case 441:
                return "uul";
            case 442:
                return "uar";
            case 443:
                return "utp";
            case 444:
                return "uuc";
            case 445:
                return "uml";
            case 446:
                return "urg";
            case 447:
                return "uit";
            case 448:
                return "uow";
            case 449:
                return "uts";
            case 450:
                return "ulg";
            case 451:
                return "uvg";
            case 452:
                return "umg";
            case 453:
                return "utg";
            case 454:
                return "uhg";
            case 455:
                return "ulb";
            case 456:
                return "uvb";
            case 457:
                return "umb";
            case 458:
                return "utb";
            case 459:
                return "uhb";
            case 460:
                return "ulc";
            case 461:
                return "uvc";
            case 462:
                return "umc";
            case 463:
                return "utc";
            case 464:
                return "uhc";
            case 465:
                return "uh9";
            case 466:
                return "ush";
            case 467:
                return "upk";
            case 468:
                return "dr6";
            case 469:
                return "dr7";
            case 470:
                return "dr8";
            case 471:
                return "dr9";
            case 472:
                return "dra";
            case 473:
                return "ba6";
            case 474:
                return "ba7";
            case 475:
                return "ba8";
            case 476:
                return "ba9";
            case 477:
                return "baa";
            case 478:
                return "pa6";
            case 479:
                return "pa7";
            case 480:
                return "pa8";
            case 481:
                return "pa9";
            case 482:
                return "paa";
            case 483:
                return "ne6";
            case 484:
                return "ne7";
            case 485:
                return "ne8";
            case 486:
                return "ne9";
            case 487:
                return "nea";
            case 488:
                return "drb";
            case 489:
                return "drc";
            case 490:
                return "drd";
            case 491:
                return "dre";
            case 492:
                return "drf";
            case 493:
                return "bab";
            case 494:
                return "bac";
            case 495:
                return "bad";
            case 496:
                return "bae";
            case 497:
                return "baf";
            case 498:
                return "pab";
            case 499:
                return "pac";
            case 500:
                return "pad";
            case 501:
                return "pae";
            case 502:
                return "paf";
            case 503:
                return "neb";
            case 504:
                return "neg";
            case 505:
                return "ned";
            case 506:
                return "nee";
            case 507:
                return "nef";
            case 508:
                return "elx";
            case 509:
                return "hpo";
            case 510:
                return "mpo";
            case 511:
                return "hpf";
            case 512:
                return "mpf";
            case 513:
                return "vps";
            case 514:
                return "yps";
            case 515:
                return "rvs";
            case 516:
                return "rvl";
            case 517:
                return "wms";
            case 518:
                return "tbk";
            case 519:
                return "ibk";
            case 520:
                return "amu";
            case 521:
                return "vip";
            case 522:
                return "rin";
            case 523:
                return "gld";
            case 524:
                return "bks";
            case 525:
                return "bkd";
            case 526:
                return "aqv";
            case 527:
                return "tch";
            case 528:
                return "cqv";
            case 529:
                return "tsc";
            case 530:
                return "isc";
            case 531:
                return "hrt";
            case 532:
                return "brz";
            case 533:
                return "jaw";
            case 534:
                return "eyz";
            case 535:
                return "hrn";
            case 536:
                return "tal";
            case 537:
                return "flg";
            case 538:
                return "fng";
            case 539:
                return "qll";
            case 540:
                return "sol";
            case 541:
                return "scz";
            case 542:
                return "spe";
            case 543:
                return "key";
            case 544:
                return "luv";
            case 545:
                return "xyz";
            case 546:
                return "j34";
            case 547:
                return "g34";
            case 548:
                return "bbb";
            case 549:
                return "box";
            case 550:
                return "tr1";
            case 551:
                return "mss";
            case 552:
                return "ass";
            case 553:
                return "qey";
            case 554:
                return "qhr";
            case 555:
                return "qbr";
            case 556:
                return "ear";
            case 557:
                return "gcv";
            case 558:
                return "gfv";
            case 559:
                return "gsv";
            case 560:
                return "gzv";
            case 561:
                return "gpv";
            case 562:
                return "gcy";
            case 563:
                return "gfy";
            case 564:
                return "gsy";
            case 565:
                return "gly";
            case 566:
                return "gpy";
            case 567:
                return "gcb";
            case 568:
                return "gfb";
            case 569:
                return "gsb";
            case 570:
                return "glb";
            case 571:
                return "gpb";
            case 572:
                return "gcg";
            case 573:
                return "gfg";
            case 574:
                return "gsg";
            case 575:
                return "glg";
            case 576:
                return "gpg";
            case 577:
                return "gcr";
            case 578:
                return "gfr";
            case 579:
                return "gsr";
            case 580:
                return "glr";
            case 581:
                return "gpr";
            case 582:
                return "gcw";
            case 583:
                return "gfw";
            case 584:
                return "gsw";
            case 585:
                return "glw";
            case 586:
                return "gpw";
            case 587:
                return "hp1";
            case 588:
                return "hp2";
            case 589:
                return "hp3";
            case 590:
                return "hp4";
            case 591:
                return "hp5";
            case 592:
                return "mp1";
            case 593:
                return "mp2";
            case 594:
                return "mp3";
            case 595:
                return "mp4";
            case 596:
                return "mp5";
            case 597:
                return "skc";
            case 598:
                return "skf";
            case 599:
                return "sku";
            case 600:
                return "skl";
            case 601:
                return "skz";
            case 602:
                return "hrb";
            case 603:
                return "cm1";
            case 604:
                return "cm2";
            case 605:
                return "cm3";
            case 606:
                return "rps";
            case 607:
                return "rpl";
            case 608:
                return "bps";
            case 609:
                return "bpl";
            case 610:
                return "r01";
            case 611:
                return "r02";
            case 612:
                return "r03";
            case 613:
                return "r04";
            case 614:
                return "r05";
            case 615:
                return "r06";
            case 616:
                return "r07";
            case 617:
                return "r08";
            case 618:
                return "r09";
            case 619:
                return "r10";
            case 620:
                return "r11";
            case 621:
                return "r12";
            case 622:
                return "r13";
            case 623:
                return "r14";
            case 624:
                return "r15";
            case 625:
                return "r16";
            case 626:
                return "r17";
            case 627:
                return "r18";
            case 628:
                return "r19";
            case 629:
                return "r20";
            case 630:
                return "r21";
            case 631:
                return "r22";
            case 632:
                return "r23";
            case 633:
                return "r24";
            case 634:
                return "r25";
            case 635:
                return "r26";
            case 636:
                return "r27";
            case 637:
                return "r28";
            case 638:
                return "r29";
            case 639:
                return "r30";
            case 640:
                return "r31";
            case 641:
                return "r32";
            case 642:
                return "r33";
            case 643:
                return "jew";
            case 644:
                return "ice";
            case 645:
                return "0sc";
            case 646:
                return "tr2";
            case 647:
                return "pk1";
            case 648:
                return "pk2";
            case 649:
                return "pk3";
            case 650:
                return "dhn";
            case 651:
                return "bey";
            case 652:
                return "mbr";
            case 653:
                return "toa";
            case 654:
                return "tes";
            case 655:
                return "ceh";
            case 656:
                return "bet";
            case 657:
                return "fed";
            case 658:
                return "std";
            default:
                return "";
        }
    }

    string getItemCode(int txtFileNo)
    {
        switch (txtFileNo)
        {
            case 0:
                return "hax";
            case 1:
                return "axe";
            case 2:
                return "2ax";
            case 3:
                return "mpi";
            case 4:
                return "wax";
            case 5:
                return "lax";
            case 6:
                return "bax";
            case 7:
                return "btx";
            case 8:
                return "gax";
            case 9:
                return "gix";
            case 10:
                return "wnd";
            case 11:
                return "ywn";
            case 12:
                return "bwn";
            case 13:
                return "gwn";
            case 14:
                return "clb";
            case 15:
                return "scp";
            case 16:
                return "gsc";
            case 17:
                return "wsp";
            case 18:
                return "spc";
            case 19:
                return "mac";
            case 20:
                return "mst";
            case 21:
                return "fla";
            case 22:
                return "whm";
            case 23:
                return "mau";
            case 24:
                return "gma";
            case 25:
                return "ssd";
            case 26:
                return "scm";
            case 27:
                return "sbr";
            case 28:
                return "flc";
            case 29:
                return "crs";
            case 30:
                return "bsd";
            case 31:
                return "lsd";
            case 32:
                return "wsd";
            case 33:
                return "2hs";
            case 34:
                return "clm";
            case 35:
                return "gis";
            case 36:
                return "bsw";
            case 37:
                return "flb";
            case 38:
                return "gsd";
            case 39:
                return "dgr";
            case 40:
                return "dir";
            case 41:
                return "kri";
            case 42:
                return "bld";
            case 43:
                return "tkf";
            case 44:
                return "tax";
            case 45:
                return "bkf";
            case 46:
                return "bal";
            case 47:
                return "jav";
            case 48:
                return "pil";
            case 49:
                return "ssp";
            case 50:
                return "glv";
            case 51:
                return "tsp";
            case 52:
                return "spr";
            case 53:
                return "tri";
            case 54:
                return "brn";
            case 55:
                return "spt";
            case 56:
                return "pik";
            case 57:
                return "bar";
            case 58:
                return "vou";
            case 59:
                return "scy";
            case 60:
                return "pax";
            case 61:
                return "hal";
            case 62:
                return "wsc";
            case 63:
                return "sst";
            case 64:
                return "lst";
            case 65:
                return "cst";
            case 66:
                return "bst";
            case 67:
                return "wst";
            case 68:
                return "sbw";
            case 69:
                return "hbw";
            case 70:
                return "lbw";
            case 71:
                return "cbw";
            case 72:
                return "sbb";
            case 73:
                return "lbb";
            case 74:
                return "swb";
            case 75:
                return "lwb";
            case 76:
                return "lxb";
            case 77:
                return "mxb";
            case 78:
                return "hxb";
            case 79:
                return "rxb";
            case 80:
                return "gps";
            case 81:
                return "ops";
            case 82:
                return "gpm";
            case 83:
                return "opm";
            case 84:
                return "gpl";
            case 85:
                return "opl";
            case 86:
                return "d33";
            case 87:
                return "g33";
            case 88:
                return "leg";
            case 89:
                return "hdm";
            case 90:
                return "hfh";
            case 91:
                return "hst";
            case 92:
                return "msf";
            case 93:
                return "9ha";
            case 94:
                return "9ax";
            case 95:
                return "92a";
            case 96:
                return "9mp";
            case 97:
                return "9wa";
            case 98:
                return "9la";
            case 99:
                return "9ba";
            case 100:
                return "9bt";
            case 101:
                return "9ga";
            case 102:
                return "9gi";
            case 103:
                return "9wn";
            case 104:
                return "9yw";
            case 105:
                return "9bw";
            case 106:
                return "9gw";
            case 107:
                return "9cl";
            case 108:
                return "9sc";
            case 109:
                return "9qs";
            case 110:
                return "9ws";
            case 111:
                return "9sp";
            case 112:
                return "9ma";
            case 113:
                return "9mt";
            case 114:
                return "9fl";
            case 115:
                return "9wh";
            case 116:
                return "9m9";
            case 117:
                return "9gm";
            case 118:
                return "9ss";
            case 119:
                return "9sm";
            case 120:
                return "9sb";
            case 121:
                return "9fc";
            case 122:
                return "9cr";
            case 123:
                return "9bs";
            case 124:
                return "9ls";
            case 125:
                return "9wd";
            case 126:
                return "92h";
            case 127:
                return "9cm";
            case 128:
                return "9gs";
            case 129:
                return "9b9";
            case 130:
                return "9fb";
            case 131:
                return "9gd";
            case 132:
                return "9dg";
            case 133:
                return "9di";
            case 134:
                return "9kr";
            case 135:
                return "9bl";
            case 136:
                return "9tk";
            case 137:
                return "9ta";
            case 138:
                return "9bk";
            case 139:
                return "9b8";
            case 140:
                return "9ja";
            case 141:
                return "9pi";
            case 142:
                return "9s9";
            case 143:
                return "9gl";
            case 144:
                return "9ts";
            case 145:
                return "9sr";
            case 146:
                return "9tr";
            case 147:
                return "9br";
            case 148:
                return "9st";
            case 149:
                return "9p9";
            case 150:
                return "9b7";
            case 151:
                return "9vo";
            case 152:
                return "9s8";
            case 153:
                return "9pa";
            case 154:
                return "9h9";
            case 155:
                return "9wc";
            case 156:
                return "8ss";
            case 157:
                return "8ls";
            case 158:
                return "8cs";
            case 159:
                return "8bs";
            case 160:
                return "8ws";
            case 161:
                return "8sb";
            case 162:
                return "8hb";
            case 163:
                return "8lb";
            case 164:
                return "8cb";
            case 165:
                return "8s8";
            case 166:
                return "8l8";
            case 167:
                return "8sw";
            case 168:
                return "8lw";
            case 169:
                return "8lx";
            case 170:
                return "8mx";
            case 171:
                return "8hx";
            case 172:
                return "8rx";
            case 173:
                return "qf1";
            case 174:
                return "qf2";
            case 175:
                return "ktr";
            case 176:
                return "wrb";
            case 177:
                return "axf";
            case 178:
                return "ces";
            case 179:
                return "clw";
            case 180:
                return "btl";
            case 181:
                return "skr";
            case 182:
                return "9ar";
            case 183:
                return "9wb";
            case 184:
                return "9xf";
            case 185:
                return "9cs";
            case 186:
                return "9lw";
            case 187:
                return "9tw";
            case 188:
                return "9qr";
            case 189:
                return "7ar";
            case 190:
                return "7wb";
            case 191:
                return "7xf";
            case 192:
                return "7cs";
            case 193:
                return "7lw";
            case 194:
                return "7tw";
            case 195:
                return "7qr";
            case 196:
                return "7ha";
            case 197:
                return "7ax";
            case 198:
                return "72a";
            case 199:
                return "7mp";
            case 200:
                return "7wa";
            case 201:
                return "7la";
            case 202:
                return "7ba";
            case 203:
                return "7bt";
            case 204:
                return "7ga";
            case 205:
                return "7gi";
            case 206:
                return "7wn";
            case 207:
                return "7yw";
            case 208:
                return "7bw";
            case 209:
                return "7gw";
            case 210:
                return "7cl";
            case 211:
                return "7sc";
            case 212:
                return "7qs";
            case 213:
                return "7ws";
            case 214:
                return "7sp";
            case 215:
                return "7ma";
            case 216:
                return "7mt";
            case 217:
                return "7fl";
            case 218:
                return "7wh";
            case 219:
                return "7m7";
            case 220:
                return "7gm";
            case 221:
                return "7ss";
            case 222:
                return "7sm";
            case 223:
                return "7sb";
            case 224:
                return "7fc";
            case 225:
                return "7cr";
            case 226:
                return "7bs";
            case 227:
                return "7ls";
            case 228:
                return "7wd";
            case 229:
                return "72h";
            case 230:
                return "7cm";
            case 231:
                return "7gs";
            case 232:
                return "7b7";
            case 233:
                return "7fb";
            case 234:
                return "7gd";
            case 235:
                return "7dg";
            case 236:
                return "7di";
            case 237:
                return "7kr";
            case 238:
                return "7bl";
            case 239:
                return "7tk";
            case 240:
                return "7ta";
            case 241:
                return "7bk";
            case 242:
                return "7b8";
            case 243:
                return "7ja";
            case 244:
                return "7pi";
            case 245:
                return "7s7";
            case 246:
                return "7gl";
            case 247:
                return "7ts";
            case 248:
                return "7sr";
            case 249:
                return "7tr";
            case 250:
                return "7br";
            case 251:
                return "7st";
            case 252:
                return "7p7";
            case 253:
                return "7o7";
            case 254:
                return "7vo";
            case 255:
                return "7s8";
            case 256:
                return "7pa";
            case 257:
                return "7h7";
            case 258:
                return "7wc";
            case 259:
                return "6ss";
            case 260:
                return "6ls";
            case 261:
                return "6cs";
            case 262:
                return "6bs";
            case 263:
                return "6ws";
            case 264:
                return "6sb";
            case 265:
                return "6hb";
            case 266:
                return "6lb";
            case 267:
                return "6cb";
            case 268:
                return "6s7";
            case 269:
                return "6l7";
            case 270:
                return "6sw";
            case 271:
                return "6lw";
            case 272:
                return "6lx";
            case 273:
                return "6mx";
            case 274:
                return "6hx";
            case 275:
                return "6rx";
            case 276:
                return "ob1";
            case 277:
                return "ob2";
            case 278:
                return "ob3";
            case 279:
                return "ob4";
            case 280:
                return "ob5";
            case 281:
                return "am1";
            case 282:
                return "am2";
            case 283:
                return "am3";
            case 284:
                return "am4";
            case 285:
                return "am5";
            case 286:
                return "ob6";
            case 287:
                return "ob7";
            case 288:
                return "ob8";
            case 289:
                return "ob9";
            case 290:
                return "oba";
            case 291:
                return "am6";
            case 292:
                return "am7";
            case 293:
                return "am8";
            case 294:
                return "am9";
            case 295:
                return "ama";
            case 296:
                return "obb";
            case 297:
                return "obc";
            case 298:
                return "obd";
            case 299:
                return "obe";
            case 300:
                return "obf";
            case 301:
                return "amb";
            case 302:
                return "amc";
            case 303:
                return "amd";
            case 304:
                return "ame";
            case 305:
                return "amf";
            case 306:
                return "cap";
            case 307:
                return "skp";
            case 308:
                return "hlm";
            case 309:
                return "fhl";
            case 310:
                return "ghm";
            case 311:
                return "crn";
            case 312:
                return "msk";
            case 313:
                return "qui";
            case 314:
                return "lea";
            case 315:
                return "hla";
            case 316:
                return "stu";
            case 317:
                return "rng";
            case 318:
                return "scl";
            case 319:
                return "chn";
            case 320:
                return "brs";
            case 321:
                return "spl";
            case 322:
                return "plt";
            case 323:
                return "fld";
            case 324:
                return "gth";
            case 325:
                return "ful";
            case 326:
                return "aar";
            case 327:
                return "ltp";
            case 328:
                return "buc";
            case 329:
                return "sml";
            case 330:
                return "lrg";
            case 331:
                return "kit";
            case 332:
                return "tow";
            case 333:
                return "gts";
            case 334:
                return "lgl";
            case 335:
                return "vgl";
            case 336:
                return "mgl";
            case 337:
                return "tgl";
            case 338:
                return "hgl";
            case 339:
                return "lbt";
            case 340:
                return "vbt";
            case 341:
                return "mbt";
            case 342:
                return "tbt";
            case 343:
                return "hbt";
            case 344:
                return "lbl";
            case 345:
                return "vbl";
            case 346:
                return "mbl";
            case 347:
                return "tbl";
            case 348:
                return "hbl";
            case 349:
                return "bhm";
            case 350:
                return "bsh";
            case 351:
                return "spk";
            case 352:
                return "xap";
            case 353:
                return "xkp";
            case 354:
                return "xlm";
            case 355:
                return "xhl";
            case 356:
                return "xhm";
            case 357:
                return "xrn";
            case 358:
                return "xsk";
            case 359:
                return "xui";
            case 360:
                return "xea";
            case 361:
                return "xla";
            case 362:
                return "xtu";
            case 363:
                return "xng";
            case 364:
                return "xcl";
            case 365:
                return "xhn";
            case 366:
                return "xrs";
            case 367:
                return "xpl";
            case 368:
                return "xlt";
            case 369:
                return "xld";
            case 370:
                return "xth";
            case 371:
                return "xul";
            case 372:
                return "xar";
            case 373:
                return "xtp";
            case 374:
                return "xuc";
            case 375:
                return "xml";
            case 376:
                return "xrg";
            case 377:
                return "xit";
            case 378:
                return "xow";
            case 379:
                return "xts";
            case 380:
                return "xlg";
            case 381:
                return "xvg";
            case 382:
                return "xmg";
            case 383:
                return "xtg";
            case 384:
                return "xhg";
            case 385:
                return "xlb";
            case 386:
                return "xvb";
            case 387:
                return "xmb";
            case 388:
                return "xtb";
            case 389:
                return "xhb";
            case 390:
                return "zlb";
            case 391:
                return "zvb";
            case 392:
                return "zmb";
            case 393:
                return "ztb";
            case 394:
                return "zhb";
            case 395:
                return "xh9";
            case 396:
                return "xsh";
            case 397:
                return "xpk";
            case 398:
                return "dr1";
            case 399:
                return "dr2";
            case 400:
                return "dr3";
            case 401:
                return "dr4";
            case 402:
                return "dr5";
            case 403:
                return "ba1";
            case 404:
                return "ba2";
            case 405:
                return "ba3";
            case 406:
                return "ba4";
            case 407:
                return "ba5";
            case 408:
                return "pa1";
            case 409:
                return "pa2";
            case 410:
                return "pa3";
            case 411:
                return "pa4";
            case 412:
                return "pa5";
            case 413:
                return "ne1";
            case 414:
                return "ne2";
            case 415:
                return "ne3";
            case 416:
                return "ne4";
            case 417:
                return "ne5";
            case 418:
                return "ci0";
            case 419:
                return "ci1";
            case 420:
                return "ci2";
            case 421:
                return "ci3";
            case 422:
                return "uap";
            case 423:
                return "ukp";
            case 424:
                return "ulm";
            case 425:
                return "uhl";
            case 426:
                return "uhm";
            case 427:
                return "urn";
            case 428:
                return "usk";
            case 429:
                return "uui";
            case 430:
                return "uea";
            case 431:
                return "ula";
            case 432:
                return "utu";
            case 433:
                return "ung";
            case 434:
                return "ucl";
            case 435:
                return "uhn";
            case 436:
                return "urs";
            case 437:
                return "upl";
            case 438:
                return "ult";
            case 439:
                return "uld";
            case 440:
                return "uth";
            case 441:
                return "uul";
            case 442:
                return "uar";
            case 443:
                return "utp";
            case 444:
                return "uuc";
            case 445:
                return "uml";
            case 446:
                return "urg";
            case 447:
                return "uit";
            case 448:
                return "uow";
            case 449:
                return "uts";
            case 450:
                return "ulg";
            case 451:
                return "uvg";
            case 452:
                return "umg";
            case 453:
                return "utg";
            case 454:
                return "uhg";
            case 455:
                return "ulb";
            case 456:
                return "uvb";
            case 457:
                return "umb";
            case 458:
                return "utb";
            case 459:
                return "uhb";
            case 460:
                return "ulc";
            case 461:
                return "uvc";
            case 462:
                return "umc";
            case 463:
                return "utc";
            case 464:
                return "uhc";
            case 465:
                return "uh9";
            case 466:
                return "ush";
            case 467:
                return "upk";
            case 468:
                return "dr6";
            case 469:
                return "dr7";
            case 470:
                return "dr8";
            case 471:
                return "dr9";
            case 472:
                return "dra";
            case 473:
                return "ba6";
            case 474:
                return "ba7";
            case 475:
                return "ba8";
            case 476:
                return "ba9";
            case 477:
                return "baa";
            case 478:
                return "pa6";
            case 479:
                return "pa7";
            case 480:
                return "pa8";
            case 481:
                return "pa9";
            case 482:
                return "paa";
            case 483:
                return "ne6";
            case 484:
                return "ne7";
            case 485:
                return "ne8";
            case 486:
                return "ne9";
            case 487:
                return "nea";
            case 488:
                return "drb";
            case 489:
                return "drc";
            case 490:
                return "drd";
            case 491:
                return "dre";
            case 492:
                return "drf";
            case 493:
                return "bab";
            case 494:
                return "bac";
            case 495:
                return "bad";
            case 496:
                return "bae";
            case 497:
                return "baf";
            case 498:
                return "pab";
            case 499:
                return "pac";
            case 500:
                return "pad";
            case 501:
                return "pae";
            case 502:
                return "paf";
            case 503:
                return "neb";
            case 504:
                return "neg";
            case 505:
                return "ned";
            case 506:
                return "nee";
            case 507:
                return "nef";
            case 508:
                return "elx";
            case 509:
                return "hpo";
            case 510:
                return "mpo";
            case 511:
                return "hpf";
            case 512:
                return "mpf";
            case 513:
                return "vps";
            case 514:
                return "yps";
            case 515:
                return "rvs";
            case 516:
                return "rvl";
            case 517:
                return "wms";
            case 518:
                return "tbk";
            case 519:
                return "ibk";
            case 520:
                return "amu";
            case 521:
                return "vip";
            case 522:
                return "rin";
            case 523:
                return "gld";
            case 524:
                return "bks";
            case 525:
                return "bkd";
            case 526:
                return "aqv";
            case 527:
                return "tch";
            case 528:
                return "cqv";
            case 529:
                return "tsc";
            case 530:
                return "isc";
            case 531:
                return "hrt";
            case 532:
                return "brz";
            case 533:
                return "jaw";
            case 534:
                return "eyz";
            case 535:
                return "hrn";
            case 536:
                return "tal";
            case 537:
                return "flg";
            case 538:
                return "fng";
            case 539:
                return "qll";
            case 540:
                return "sol";
            case 541:
                return "scz";
            case 542:
                return "spe";
            case 543:
                return "key";
            case 544:
                return "luv";
            case 545:
                return "xyz";
            case 546:
                return "j34";
            case 547:
                return "g34";
            case 548:
                return "bbb";
            case 549:
                return "box";
            case 550:
                return "tr1";
            case 551:
                return "mss";
            case 552:
                return "ass";
            case 553:
                return "qey";
            case 554:
                return "qhr";
            case 555:
                return "qbr";
            case 556:
                return "ear";
            case 557:
                return "gcv";
            case 558:
                return "gfv";
            case 559:
                return "gsv";
            case 560:
                return "gzv";
            case 561:
                return "gpv";
            case 562:
                return "gcy";
            case 563:
                return "gfy";
            case 564:
                return "gsy";
            case 565:
                return "gly";
            case 566:
                return "gpy";
            case 567:
                return "gcb";
            case 568:
                return "gfb";
            case 569:
                return "gsb";
            case 570:
                return "glb";
            case 571:
                return "gpb";
            case 572:
                return "gcg";
            case 573:
                return "gfg";
            case 574:
                return "gsg";
            case 575:
                return "glg";
            case 576:
                return "gpg";
            case 577:
                return "gcr";
            case 578:
                return "gfr";
            case 579:
                return "gsr";
            case 580:
                return "glr";
            case 581:
                return "gpr";
            case 582:
                return "gcw";
            case 583:
                return "gfw";
            case 584:
                return "gsw";
            case 585:
                return "glw";
            case 586:
                return "gpw";
            case 587:
                return "hp1";
            case 588:
                return "hp2";
            case 589:
                return "hp3";
            case 590:
                return "hp4";
            case 591:
                return "hp5";
            case 592:
                return "mp1";
            case 593:
                return "mp2";
            case 594:
                return "mp3";
            case 595:
                return "mp4";
            case 596:
                return "mp5";
            case 597:
                return "skc";
            case 598:
                return "skf";
            case 599:
                return "sku";
            case 600:
                return "skl";
            case 601:
                return "skz";
            case 602:
                return "hrb";
            case 603:
                return "cm1";
            case 604:
                return "cm2";
            case 605:
                return "cm3";
            case 606:
                return "rps";
            case 607:
                return "rpl";
            case 608:
                return "bps";
            case 609:
                return "bpl";
            case 610:
                return "r01";
            case 611:
                return "r02";
            case 612:
                return "r03";
            case 613:
                return "r04";
            case 614:
                return "r05";
            case 615:
                return "r06";
            case 616:
                return "r07";
            case 617:
                return "r08";
            case 618:
                return "r09";
            case 619:
                return "r10";
            case 620:
                return "r11";
            case 621:
                return "r12";
            case 622:
                return "r13";
            case 623:
                return "r14";
            case 624:
                return "r15";
            case 625:
                return "r16";
            case 626:
                return "r17";
            case 627:
                return "r18";
            case 628:
                return "r19";
            case 629:
                return "r20";
            case 630:
                return "r21";
            case 631:
                return "r22";
            case 632:
                return "r23";
            case 633:
                return "r24";
            case 634:
                return "r25";
            case 635:
                return "r26";
            case 636:
                return "r27";
            case 637:
                return "r28";
            case 638:
                return "r29";
            case 639:
                return "r30";
            case 640:
                return "r31";
            case 641:
                return "r32";
            case 642:
                return "r33";
            case 643:
                return "jew";
            case 644:
                return "ice";
            case 645:
                return "0sc";
            case 646:
                return "tr2";
            case 647:
                return "pk1";
            case 648:
                return "pk2";
            case 649:
                return "pk3";
            case 650:
                return "dhn";
            case 651:
                return "bey";
            case 652:
                return "mbr";
            case 653:
                return "toa";
            case 654:
                return "tes";
            case 655:
                return "ceh";
            case 656:
                return "bet";
            case 657:
                return "fed";
            case 658:
                return "std";
            default:
                return "";
        }
    }

    string getSetItemName(string itemCode)
    {
        switch (itemCode)
        {
            case "lrg":
                return "Civerb's Ward";
            case "gsc":
                return "Civerb's Cudgel";
            case "mbt":
                return "Hsarus' Iron Heel";
            case "buc":
                return "Hsarus' Iron Fist";
            case "lsd":
                return "Cleglaw's Tooth";
            case "sml":
                return "Cleglaw's Claw";
            case "mgl":
                return "Cleglaw's Pincers";
            case "bsd":
                return "Isenhart's Lightbrand";
            case "gts":
                return "Isenhart's Parry";
            case "brs":
                return "Isenhart's Case";
            case "fhl":
                return "Isenhart's Horns";
            case "lbb":
                return "Vidala's Barb";
            case "tbt":
                return "Vidala's Fetlock";
            case "lea":
                return "Vidala's Ambush";
            case "kit":
                return "Milabrega's Orb";
            case "wsp":
                return "Milabrega's Rod";
            case "aar":
                return "Milabrega's Robe";
            case "bst":
                return "Cathan's Rule";
            case "chn":
                return "Cathan's Mesh";
            case "msk":
                return "Cathan's Visage";
            case "mpi":
                return "Tancred's Crowbill";
            case "ful":
                return "Tancred's Spine";
            case "lbt":
                return "Tancred's Hobnails";
            case "bhm":
                return "Tancred's Skull";
            case "hgl":
                return "Sigon's Gage";
            case "ghm":
                return "Sigon's Visor";
            case "gth":
                return "Sigon's Shelter";
            case "hbt":
                return "Sigon's Sabot";
            case "hbl":
                return "Sigon's Wrap";
            case "tow":
                return "Sigon's Guard";
            case "gwn":
                return "Infernal Torch";
            case "hlm":
                return "Berserker's Headgear";
            case "spl":
                return "Berserker's Hauberk";
            case "2ax":
                return "Berserker's Hatchet";
            case "lgl":
                return "Death's Hand";
            case "lbl":
                return "Death's Guard";
            case "wsd":
                return "Death's Touch";
            case "sbr":
                return "Angelic Sickle";
            case "rng":
                return "Angelic Mantle";
            case "swb":
                return "Arctic Horn";
            case "qui":
                return "Arctic Furs";
            case "vbl":
                return "Arctic Binding";
            case "wst":
                return "Arcanna's Deathwand";
            case "skp":
                return "Arcanna's Head";
            case "ltp":
                return "Arcanna's Flesh";
            case "xh9":
                return "Natalya's Totem";
            case "7qr":
                return "Natalya's Mark";
            case "ucl":
                return "Natalya's Shadow";
            case "xmb":
                return "Natalya's Soul";
            case "dr8":
                return "Aldur's Stony Gaze";
            case "uul":
                return "Aldur's Deception";
            case "9mt":
                return "Aldur's Gauntlet";
            case "xtb":
                return "Aldur's Advance";
            case "ba5":
                return "Immortal King's Will";
            case "uar":
                return "Immortal King's Soul Cage";
            case "zhb":
                return "Immortal King's Detail";
            case "xhg":
                return "Immortal King's Forge";
            case "xhb":
                return "Immortal King's Pillar";
            case "7m7":
                return "Immortal King's Stone Crusher";
            case "zmb":
                return "Tal Rasha's Fire-Spun Cloth";
            case "oba":
                return "Tal Rasha's Lidless Eye";
            case "uth":
                return "Tal Rasha's Howling Wind";
            case "xsk":
                return "Tal Rasha's Horadric Crest";
            case "urn":
                return "Griswold's Valor";
            case "xar":
                return "Griswold's Heart";
            case "7ws":
                return "Griswolds's Redemption";
            case "paf":
                return "Griswold's Honor";
            case "uh9":
                return "Trang-Oul's Guise";
            case "xul":
                return "Trang-Oul's Scales";
            case "ne9":
                return "Trang-Oul's Wing";
            case "xmg":
                return "Trang-Oul's Claws";
            case "utc":
                return "Trang-Oul's Girth";
            case "ci3":
                return "M'avina's True Sight";
            case "uld":
                return "M'avina's Embrace";
            case "xtg":
                return "M'avina's Icy Clutch";
            case "zvb":
                return "M'avina's Tenet";
            case "amc":
                return "M'avina's Caster";
            case "ulg":
                return "Laying of Hands";
            case "xlb":
                return "Rite of Passage";
            case "uui":
                return "Spiritual Custodian";
            case "umc":
                return "Credendum";
            case "7ma":
                return "Dangoon's Teaching";
            case "uts":
                return "Heaven's Taebaek";
            case "xrs":
                return "Haemosu's Adament";
            case "uhm":
                return "Ondal's Almighty";
            case "xhm":
                return "Guillaume's Face";
            case "ztb":
                return "Wilhelm's Pride";
            case "xvg":
                return "Magnus' Skin";
            case "xml":
                return "Wihtstan's Guard";
            case "xrn":
                return "Hwanin's Splendor";
            case "xcl":
                return "Hwanin's Refuge";
            case "9vo":
                return "Hwanin's Justice";
            case "7ls":
                return "Sazabi's Cobalt Redeemer";
            case "upl":
                return "Sazabi's Ghost Liberator";
            case "xhl":
                return "Sazabi's Mental Sheath";
            case "7gd":
                return "Bul-Kathos' Sacred Charge";
            case "7wd":
                return "Bul-Kathos' Tribal Guardian";
            case "xap":
                return "Cow King's Horns";
            case "stu":
                return "Cow King's Hide";
            case "6cs":
                return "Naj's Puzzler";
            case "ult":
                return "Naj's Light Plate";
            case "ci0":
                return "Naj's Circlet";
            case "vgl":
                return "McAuley's Taboo";
            case "bwn":
                return "McAuley's Superstition";
            default:
                return "";
        }
    }

    string getUniqueItemName(string itemCode)
    {
        switch (itemCode)
        {
            case "hax":
                return "The Gnasher";
            case "axe":
                return "Deathspade";
            case "2ax":
                return "Bladebone";
            case "mpi":
                return "Mindrend";
            case "wax":
                return "Rakescar";
            case "lax":
                return "Fechmars Axe";
            case "bax":
                return "Goreshovel";
            case "btx":
                return "The Chieftan";
            case "gax":
                return "Brainhew";
            case "gix":
                return "The Humongous";
            case "wnd":
                return "Iros Torch";
            case "ywn":
                return "Maelstromwrath";
            case "bwn":
                return "Gravenspine";
            case "gwn":
                return "Umes Lament";
            case "clb":
                return "Felloak";
            case "scp":
                return "Knell Striker";
            case "gsc":
                return "Rusthandle";
            case "wsp":
                return "Stormeye";
            case "spc":
                return "Stoutnail";
            case "mac":
                return "Crushflange";
            case "mst":
                return "Bloodrise";
            case "fla":
                return "The Generals Tan Do Li Ga";
            case "whm":
                return "Ironstone";
            case "mau":
                return "Bonesob";
            case "gma":
                return "Steeldriver";
            case "ssd":
                return "Rixots Keen";
            case "scm":
                return "Blood Crescent";
            case "sbr":
                return "Krintizs Skewer";
            case "flc":
                return "Gleamscythe";
            case "crs":
                return "Azurewrath";
            case "bsd":
                return "Griswolds Edge";
            case "lsd":
                return "Hellplague";
            case "wsd":
                return "Culwens Point";
            case "2hs":
                return "Shadowfang";
            case "clm":
                return "Soulflay";
            case "gis":
                return "Kinemils Awl";
            case "bsw":
                return "Blacktongue";
            case "flb":
                return "Ripsaw";
            case "gsd":
                return "The Patriarch";
            case "dgr":
                return "Gull";
            case "dir":
                return "The Diggler";
            case "kri":
                return "The Jade Tan Do";
            case "bld":
                return "Irices Shard";
            case "spr":
                return "The Dragon Chang";
            case "tri":
                return "Razortine";
            case "brn":
                return "Bloodthief";
            case "spt":
                return "Lance of Yaggai";
            case "pik":
                return "The Tannr Gorerod";
            case "bar":
                return "Dimoaks Hew";
            case "vou":
                return "Steelgoad";
            case "scy":
                return "Soul Harvest";
            case "pax":
                return "The Battlebranch";
            case "hal":
                return "Woestave";
            case "wsc":
                return "The Grim Reaper";
            case "sst":
                return "Bane Ash";
            case "lst":
                return "Serpent Lord";
            case "cst":
                return "Lazarus Spire";
            case "bst":
                return "The Salamander";
            case "wst":
                return "The Iron Jang Bong";
            case "sbw":
                return "Pluckeye";
            case "hbw":
                return "Witherstring";
            case "lbw":
                return "Rimeraven";
            case "cbw":
                return "Piercerib";
            case "sbb":
                return "Pullspite";
            case "lbb":
                return "Wizendraw";
            case "swb":
                return "Hellclap";
            case "lwb":
                return "Blastbark";
            case "lxb":
                return "Leadcrow";
            case "mxb":
                return "Ichorsting";
            case "hxb":
                return "Hellcast";
            case "rxb":
                return "Doomspittle";
            case "cap":
                return "Biggin's Bonnet";
            case "skp":
                return "Tarnhelm";
            case "hlm":
                return "Coif of Glory";
            case "fhl":
                return "Duskdeep";
            case "bhm":
                return "Wormskull";
            case "ghm":
                return "Howltusk";
            case "crn":
                return "Undead Crown";
            case "msk":
                return "The Face of Horror";
            case "qui":
                return "Greyform";
            case "lea":
                return "Blinkbats Form";
            case "hla":
                return "The Centurion";
            case "stu":
                return "Twitchthroe";
            case "rng":
                return "Darkglow";
            case "scl":
                return "Hawkmail";
            case "chn":
                return "Sparking Mail";
            case "brs":
                return "Venomsward";
            case "spl":
                return "Iceblink";
            case "plt":
                return "Boneflesh";
            case "fld":
                return "Rockfleece";
            case "gth":
                return "Rattlecage";
            case "ful":
                return "Goldskin";
            case "aar":
                return "Victors Silk";
            case "ltp":
                return "Heavenly Garb";
            case "buc":
                return "Pelta Lunata";
            case "sml":
                return "Umbral Disk";
            case "lrg":
                return "Stormguild";
            case "bsh":
                return "Wall of the Eyeless";
            case "spk":
                return "Swordback Hold";
            case "kit":
                return "Steelclash";
            case "tow":
                return "Bverrit Keep";
            case "gts":
                return "The Ward";
            case "lgl":
                return "The Hand of Broc";
            case "vgl":
                return "Bloodfist";
            case "mgl":
                return "Chance Guards";
            case "tgl":
                return "Magefist";
            case "hgl":
                return "Frostburn";
            case "lbt":
                return "Hotspur";
            case "vbt":
                return "Gorefoot";
            case "mbt":
                return "Treads of Cthon";
            case "tbt":
                return "Goblin Toe";
            case "hbt":
                return "Tearhaunch";
            case "lbl":
                return "Lenyms Cord";
            case "vbl":
                return "Snakecord";
            case "mbl":
                return "Nightsmoke";
            case "tbl":
                return "Goldwrap";
            case "hbl":
                return "Bladebuckle";
            case "vip":
                return "Amulet of the Viper";
            case "msf":
                return "Staff of Kings";
            case "hst":
                return "Horadric Staff";
            case "hfh":
                return "Hell Forge Hammer";
            case "qf1":
                return "KhalimFlail";
            case "qf2":
                return "SuperKhalimFlail";
            case "9ha":
                return "Coldkill";
            case "9ax":
                return "Butcher's Pupil";
            case "92a":
                return "Islestrike";
            case "9mp":
                return "Pompe's Wrath";
            case "9wa":
                return "Guardian Naga";
            case "9la":
                return "Warlord's Trust";
            case "9ba":
                return "Spellsteel";
            case "9bt":
                return "Stormrider";
            case "9ga":
                return "Boneslayer Blade";
            case "9gi":
                return "The Minataur";
            case "9wn":
                return "Suicide Branch";
            case "9yw":
                return "Carin Shard";
            case "9bw":
                return "Arm of King Leoric";
            case "9gw":
                return "Blackhand Key";
            case "9cl":
                return "Dark Clan Crusher";
            case "9sc":
                return "Zakarum's Hand";
            case "9qs":
                return "The Fetid Sprinkler";
            case "9ws":
                return "Hand of Blessed Light";
            case "9sp":
                return "Fleshrender";
            case "9ma":
                return "Sureshrill Frost";
            case "9mt":
                return "Moonfall";
            case "9fl":
                return "Baezil's Vortex";
            case "9wh":
                return "Earthshaker";
            case "9m9":
                return "Bloodtree Stump";
            case "9gm":
                return "The Gavel of Pain";
            case "9ss":
                return "Bloodletter";
            case "9sm":
                return "Coldsteel Eye";
            case "9sb":
                return "Hexfire";
            case "9fc":
                return "Blade of Ali Baba";
            case "9cr":
                return "Ginther's Rift";
            case "9bs":
                return "Headstriker";
            case "9ls":
                return "Plague Bearer";
            case "9wd":
                return "The Atlantian";
            case "92h":
                return "Crainte Vomir";
            case "9cm":
                return "Bing Sz Wang";
            case "9gs":
                return "The Vile Husk";
            case "9b9":
                return "Cloudcrack";
            case "9fb":
                return "Todesfaelle Flamme";
            case "9gd":
                return "Swordguard";
            case "9dg":
                return "Spineripper";
            case "9di":
                return "Heart Carver";
            case "9kr":
                return "Blackbog's Sharp";
            case "9bl":
                return "Stormspike";
            case "9sr":
                return "The Impaler";
            case "9tr":
                return "Kelpie Snare";
            case "9br":
                return "Soulfeast Tine";
            case "9st":
                return "Hone Sundan";
            case "9p9":
                return "Spire of Honor";
            case "9b7":
                return "The Meat Scraper";
            case "9vo":
                return "Blackleach Blade";
            case "9s8":
                return "Athena's Wrath";
            case "9pa":
                return "Pierre Tombale Couant";
            case "9h9":
                return "Husoldal Evo";
            case "9wc":
                return "Grim's Burning Dead";
            case "8ss":
                return "Razorswitch";
            case "8ls":
                return "Ribcracker";
            case "8cs":
                return "Chromatic Ire";
            case "8bs":
                return "Warpspear";
            case "8ws":
                return "Skullcollector";
            case "8sb":
                return "Skystrike";
            case "8hb":
                return "Riphook";
            case "8lb":
                return "Kuko Shakaku";
            case "8cb":
                return "Endlesshail";
            case "8s8":
                return "Whichwild String";
            case "8l8":
                return "Cliffkiller";
            case "8sw":
                return "Magewrath";
            case "8lw":
                return "Godstrike Arch";
            case "8lx":
                return "Langer Briser";
            case "8mx":
                return "Pus Spiter";
            case "8hx":
                return "Buriza-Do Kyanon";
            case "8rx":
                return "Demon Machine";
            case "xap":
                return "Peasent Crown";
            case "xkp":
                return "Rockstopper";
            case "xlm":
                return "Stealskull";
            case "xhl":
                return "Darksight Helm";
            case "xhm":
                return "Valkiry Wing";
            case "xrn":
                return "Crown of Thieves";
            case "xsk":
                return "Blackhorn's Face";
            case "xh9":
                return "Vampiregaze";
            case "xui":
                return "The Spirit Shroud";
            case "xea":
                return "Skin of the Vipermagi";
            case "xla":
                return "Skin of the Flayerd One";
            case "xtu":
                return "Ironpelt";
            case "xng":
                return "Spiritforge";
            case "xcl":
                return "Crow Caw";
            case "xhn":
                return "Shaftstop";
            case "xrs":
                return "Duriel's Shell";
            case "xpl":
                return "Skullder's Ire";
            case "xlt":
                return "Guardian Angel";
            case "xld":
                return "Toothrow";
            case "xth":
                return "Atma's Wail";
            case "xul":
                return "Black Hades";
            case "xar":
                return "Corpsemourn";
            case "xtp":
                return "Que-Hegan's Wisdon";
            case "xuc":
                return "Visceratuant";
            case "xml":
                return "Mosers Blessed Circle";
            case "xrg":
                return "Stormchaser";
            case "xit":
                return "Tiamat's Rebuke";
            case "xow":
                return "Kerke's Sanctuary";
            case "xts":
                return "Radimant's Sphere";
            case "xsh":
                return "Lidless Wall";
            case "xpk":
                return "Lance Guard";
            case "xlg":
                return "Venom Grip";
            case "xvg":
                return "Gravepalm";
            case "xmg":
                return "Ghoulhide";
            case "xtg":
                return "Lavagout";
            case "xhg":
                return "Hellmouth";
            case "xlb":
                return "Infernostride";
            case "xvb":
                return "Waterwalk";
            case "xmb":
                return "Silkweave";
            case "xtb":
                return "Wartraveler";
            case "xhb":
                return "Gorerider";
            case "zlb":
                return "String of Ears";
            case "zvb":
                return "Razortail";
            case "zmb":
                return "Gloomstrap";
            case "ztb":
                return "Snowclash";
            case "zhb":
                return "Thudergod's Vigor";
            case "uap":
                return "Harlequin Crest";
            case "utu":
                return "The Gladiator's Bane";
            case "upl":
                return "Arkaine's Valor";
            case "uml":
                return "Blackoak Shield";
            case "uit":
                return "Stormshield";
            case "7bt":
                return "Hellslayer";
            case "7ga":
                return "Messerschmidt's Reaver";
            case "7mt":
                return "Baranar's Star";
            case "7b7":
                return "Doombringer";
            case "7gd":
                return "The Grandfather";
            case "7dg":
                return "Wizardspike";
            case "7wc":
                return "Stormspire";
            case "6l7":
                return "Eaglehorn";
            case "6lw":
                return "Windforce";
            case "baa":
                return "Arreat's Face";
            case "nea":
                return "Homunculus";
            case "ama":
                return "Titan's Revenge";
            case "am7":
                return "Lycander's Aim";
            case "am9":
                return "Lycander's Flank";
            case "oba":
                return "The Oculus";
            case "pa9":
                return "Herald of Zakarum";
            case "9tw":
                return "Cutthroat1";
            case "dra":
                return "Jalal's Mane";
            case "9ta":
                return "The Scalper";
            case "7sb":
                return "Bloodmoon";
            case "7sm":
                return "Djinnslayer";
            case "9tk":
                return "Deathbit";
            case "7bk":
                return "Warshrike";
            case "6rx":
                return "Gutsiphon";
            case "7ha":
                return "Razoredge";
            case "7sp":
                return "Demonlimb";
            case "7pa":
                return "Tomb Reaver";
            case "7gw":
                return "Deaths's Web";
            case "7kr":
                return "Fleshripper";
            case "7wb":
                return "Jadetalon";
            case "uhb":
                return "Shadowdancer";
            case "drb":
                return "Cerebus";
            case "umg":
                return "Souldrain";
            case "72a":
                return "Runemaster";
            case "7wa":
                return "Deathcleaver";
            case "7gi":
                return "Executioner's Justice";
            case "amd":
                return "Stoneraven";
            case "uld":
                return "Leviathan";
            case "7ts":
                return "Gargoyle's Bite";
            case "7b8":
                return "Lacerator";
            case "6ws":
                return "Mang Song's Lesson";
            case "7br":
                return "Viperfork";
            case "7ba":
                return "Ethereal Edge";
            case "bad":
                return "Demonhorn's Edge";
            case "7s8":
                return "The Reaper's Toll";
            case "drd":
                return "Spiritkeeper";
            case "6hx":
                return "Hellrack";
            case "pac":
                return "Alma Negra";
            case "nef":
                return "Darkforge Spawn";
            case "6sw":
                return "Widowmaker";
            case "amb":
                return "Bloodraven's Charge";
            case "7bl":
                return "Ghostflame";
            case "7cs":
                return "Shadowkiller";
            case "7ta":
                return "Gimmershred";
            case "ci3":
                return "Griffon's Eye";
            case "7m7":
                return "Windhammer";
            case "amf":
                return "Thunderstroke";
            case "7s7":
                return "Demon's Arch";
            case "nee":
                return "Boneflame";
            case "7p7":
                return "Steelpillar";
            case "urn":
                return "Crown of Ages";
            case "usk":
                return "Andariel's Visage";
            case "pae":
                return "Dragonscale";
            case "uul":
                return "Steel Carapice";
            case "uow":
                return "Medusa's Gaze";
            case "dre":
                return "Ravenlore";
            case "7bw":
                return "Boneshade";
            case "7gs":
                return "Flamebellow";
            case "obf":
                return "Fathom";
            case "bac":
                return "Wolfhowl";
            case "uts":
                return "Spirit Ward";
            case "ci2":
                return "Kira's Guardian";
            case "uui":
                return "Ormus' Robes";
            case "cm3":
                return "Gheed's Fortune";
            case "bae":
                return "Halaberd's Reign";
            case "upk":
                return "Spike Thorn";
            case "uvg":
                return "Dracul's Grasp";
            case "7ls":
                return "Frostwind";
            case "obc":
                return "Eschuta's temper";
            case "7lw":
                return "Firelizard's Talons";
            case "uvb":
                return "Sandstorm Trek";
            case "umb":
                return "Marrowwalk";
            case "ulc":
                return "Arachnid Mesh";
            case "uvc":
                return "Nosferatu's Coil";
            case "umc":
                return "Verdugo's Hearty Cord";
            case "uh9":
                return "Giantskull";
            case "7ws":
                return "Ironward";
            case "cm1":
                return "Annihilus";
            case "7sr":
                return "Arioc's Needle";
            case "7mp":
                return "Cranebeak";
            case "7cl":
                return "Nord's Tenderizer";
            case "7gl":
                return "Wraithflight";
            case "7o7":
                return "Bonehew";
            case "6cs":
                return "Ondal's Wisdom";
            case "ush":
                return "Headhunter's Glory";
            case "uhg":
                return "Steelrend";
            case "jew":
                return "Rainbow Facet";
            case "cm2":
                return "Hellfire Torch";
            default:
                return "";
        }
    }

    string isQuestItem(int txtFileNo)
    {
        switch (txtFileNo)
        {
            case 87:
                return "The Gidbinn";
            case 88:
                return "Wirt's Leg";
            case 89:
                return "Horadric Malus";
            case 90:
                return "Hellforge Hammer";
            case 91:
                return "Horadric Staff";
            case 92:
                return "Staff of Kings";
            case 173:
                return "Khalim's Flail";
            case 521:
                return "Amulet of the Viper";
            case 524:
                return "Scroll of Inifuss";
            case 549:
                return "Horadric Cube";
            case 550:
                return "Horadric Scroll";
            case 551:
                return "Mephisto's Soulstone";
            case 552:
                return "Book of Skill";
            case 553:
                return "Khalim's Eye";
            case 554:
                return "Khalim's Heart";
            case 555:
                return "Khalim's Brain";
            default:
                return "";
        }
    }

    public string GetItemLocation(byte ThisB)
    {
        switch (ThisB)
        {
            case 0:
                return "inventory";
            case 1:
                return "equipped";
            case 2:
                return "belt";
            case 3:
                return "ground";
            case 4:
                return "cursor";
            case 5:
                return "dropping";
            case 6:
                return "socketed";
            case 12:
                return "instore";
            default:
                return "";
        }
    }

    public string getItemBaseName(long txtFileNo)
    {
        switch (txtFileNo)
        {
            case 0:
                return "Hand Axe";
            case 1:
                return "Axe";
            case 2:
                return "Double Axe";
            case 3:
                return "Military Pick";
            case 4:
                return "War Axe";
            case 5:
                return "Large Axe";
            case 6:
                return "Broad Axe";
            case 7:
                return "Battle Axe";
            case 8:
                return "Great Axe";
            case 9:
                return "Giant Axe";
            case 10:
                return "Wand";
            case 11:
                return "Yew Wand";
            case 12:
                return "Bone Wand";
            case 13:
                return "Grim Wand";
            case 14:
                return "Club";
            case 15:
                return "Scepter";
            case 16:
                return "Grand Scepter";
            case 17:
                return "War Scepter";
            case 18:
                return "Spiked Club";
            case 19:
                return "Mace";
            case 20:
                return "Morning Star";
            case 21:
                return "Flail";
            case 22:
                return "War Hammer";
            case 23:
                return "Maul";
            case 24:
                return "Great Maul";
            case 25:
                return "Short Sword";
            case 26:
                return "Scimitar";
            case 27:
                return "Saber";
            case 28:
                return "Falchion";
            case 29:
                return "Crystal Sword";
            case 30:
                return "Broad Sword";
            case 31:
                return "Long Sword";
            case 32:
                return "War Sword";
            case 33:
                return "Two-Handed Sword";
            case 34:
                return "Claymore";
            case 35:
                return "Giant Sword";
            case 36:
                return "Bastard Sword";
            case 37:
                return "Flamberge";
            case 38:
                return "Great Sword";
            case 39:
                return "Dagger";
            case 40:
                return "Dirk";
            case 41:
                return "Kriss";
            case 42:
                return "Blade";
            case 43:
                return "Throwing Knife";
            case 44:
                return "Throwing Axe";
            case 45:
                return "Balanced Knife";
            case 46:
                return "Balanced Axe";
            case 47:
                return "Javelin";
            case 48:
                return "Pilum";
            case 49:
                return "Short Spear";
            case 50:
                return "Glaive";
            case 51:
                return "Throwing Spear";
            case 52:
                return "Spear";
            case 53:
                return "Trident";
            case 54:
                return "Brandistock";
            case 55:
                return "Spetum";
            case 56:
                return "Pike";
            case 57:
                return "Bardiche";
            case 58:
                return "Voulge";
            case 59:
                return "Scythe";
            case 60:
                return "Poleaxe";
            case 61:
                return "Halberd";
            case 62:
                return "War Scythe";
            case 63:
                return "Short Staff";
            case 64:
                return "Long Staff";
            case 65:
                return "Gnarled Staff";
            case 66:
                return "Battle Staff";
            case 67:
                return "War Staff";
            case 68:
                return "Short Bow";
            case 69:
                return "Hunter's Bow";
            case 70:
                return "Long Bow";
            case 71:
                return "Composite Bow";
            case 72:
                return "Short Battle Bow";
            case 73:
                return "Long Battle Bow";
            case 74:
                return "Short War Bow";
            case 75:
                return "Long War Bow";
            case 76:
                return "Light Crossbow";
            case 77:
                return "Crossbow";
            case 78:
                return "Heavy Crossbow";
            case 79:
                return "Repeating Crossbow";
            case 80:
                return "Rancid Gas Potion";
            case 81:
                return "Oil Potion";
            case 82:
                return "Choking Gas Potion";
            case 83:
                return "Exploding Potion";
            case 84:
                return "Strangling Gas Potion";
            case 85:
                return "Fulminating Potion";
            case 86:
                return "Decoy Gidbinn";
            case 87:
                return "The Gidbinn";
            case 88:
                return "Wirt's Leg";
            case 89:
                return "Horadric Malus";
            case 90:
                return "Hellforge Hammer";
            case 91:
                return "Horadric Staff";
            case 92:
                return "Staff of Kings";
            case 93:
                return "Hatchet";
            case 94:
                return "Cleaver";
            case 95:
                return "Twin Axe";
            case 96:
                return "Crowbill";
            case 97:
                return "Naga";
            case 98:
                return "Military Axe";
            case 99:
                return "Bearded Axe";
            case 100:
                return "Tabar";
            case 101:
                return "Gothic Axe";
            case 102:
                return "Ancient Axe";
            case 103:
                return "Burnt Wand";
            case 104:
                return "Petrified Wand";
            case 105:
                return "Tomb Wand";
            case 106:
                return "Grave Wand";
            case 107:
                return "Cudgel";
            case 108:
                return "Rune Scepter";
            case 109:
                return "Holy Water Sprinkler";
            case 110:
                return "Divine Scepter";
            case 111:
                return "Barbed Club";
            case 112:
                return "Flanged Mace";
            case 113:
                return "Jagged Star";
            case 114:
                return "Knout";
            case 115:
                return "Battle Hammer";
            case 116:
                return "War Club";
            case 117:
                return "Martel de Fer";
            case 118:
                return "Gladius";
            case 119:
                return "Cutlass";
            case 120:
                return "Shamshir";
            case 121:
                return "Tulwar";
            case 122:
                return "Dimensional Blade";
            case 123:
                return "Battle Sword";
            case 124:
                return "Rune Sword";
            case 125:
                return "Ancient Sword";
            case 126:
                return "Espandon";
            case 127:
                return "Dacian Falx";
            case 128:
                return "Tusk Sword";
            case 129:
                return "Gothic Sword";
            case 130:
                return "Zweihander";
            case 131:
                return "Executioner Sword";
            case 132:
                return "Poignard";
            case 133:
                return "Rondel";
            case 134:
                return "Cinquedeas";
            case 135:
                return "Stilleto";
            case 136:
                return "Battle Dart";
            case 137:
                return "Francisca";
            case 138:
                return "War Dart";
            case 139:
                return "Hurlbat";
            case 140:
                return "War Javelin";
            case 141:
                return "Great Pilum";
            case 142:
                return "Simbilan";
            case 143:
                return "Spiculum";
            case 144:
                return "Harpoon";
            case 145:
                return "War Spear";
            case 146:
                return "Fuscina";
            case 147:
                return "War Fork";
            case 148:
                return "Yari";
            case 149:
                return "Lance";
            case 150:
                return "Lochaber Axe";
            case 151:
                return "Bill";
            case 152:
                return "Battle Scythe";
            case 153:
                return "Partizan";
            case 154:
                return "Bec-de-Corbin";
            case 155:
                return "Grim Scythe";
            case 156:
                return "Jo Staff";
            case 157:
                return "Quarterstaff";
            case 158:
                return "Cedar Staff";
            case 159:
                return "Gothic Staff";
            case 160:
                return "Rune Staff";
            case 161:
                return "Edge Bow";
            case 162:
                return "Razor Bow";
            case 163:
                return "Cedar Bow";
            case 164:
                return "Double Bow";
            case 165:
                return "Short Siege Bow";
            case 166:
                return "Long Siege Bow";
            case 167:
                return "Rune Bow";
            case 168:
                return "Gothic Bow";
            case 169:
                return "Arbalest";
            case 170:
                return "Siege Crossbow";
            case 171:
                return "Ballista";
            case 172:
                return "Chu-Ko-Nu";
            case 173:
                return "Khalim's Flail";
            case 174:
                return "Khalim's Will";
            case 175:
                return "Katar";
            case 176:
                return "Wrist Blade";
            case 177:
                return "Hatchet Hands";
            case 178:
                return "Cestus";
            case 179:
                return "Claws";
            case 180:
                return "Blade Talons";
            case 181:
                return "Scissors Katar";
            case 182:
                return "Quhab";
            case 183:
                return "Wrist Spike";
            case 184:
                return "Fascia";
            case 185:
                return "Hand Scythe";
            case 186:
                return "Greater Claws";
            case 187:
                return "Greater Talons";
            case 188:
                return "Scissors Quhab";
            case 189:
                return "Suwayyah";
            case 190:
                return "Wrist Sword";
            case 191:
                return "War Fist";
            case 192:
                return "Battle Cestus";
            case 193:
                return "Feral Claws";
            case 194:
                return "Runic Talons";
            case 195:
                return "Scissors Suwayyah";
            case 196:
                return "Tomahawk";
            case 197:
                return "Small Crescent";
            case 198:
                return "Ettin Axe";
            case 199:
                return "War Spike";
            case 200:
                return "Berserker Axe";
            case 201:
                return "Feral Axe";
            case 202:
                return "Silver-edged Axe";
            case 203:
                return "Decapitator";
            case 204:
                return "Champion Axe";
            case 205:
                return "Glorious Axe";
            case 206:
                return "Polished Wand";
            case 207:
                return "Ghost Wand";
            case 208:
                return "Lich Wand";
            case 209:
                return "Unearthed Wand";
            case 210:
                return "Truncheon";
            case 211:
                return "Mighty Scepter";
            case 212:
                return "Seraph Rod";
            case 213:
                return "Caduceus";
            case 214:
                return "Tyrant Club";
            case 215:
                return "Reinforced Mace";
            case 216:
                return "Devil Star";
            case 217:
                return "Scourge";
            case 218:
                return "Legendary Mallet";
            case 219:
                return "Ogre Maul";
            case 220:
                return "Thunder Maul";
            case 221:
                return "Falcata";
            case 222:
                return "Ataghan";
            case 223:
                return "Elegant Blade";
            case 224:
                return "Hydra Edge";
            case 225:
                return "Phase Blade";
            case 226:
                return "Conquest Sword";
            case 227:
                return "Cryptic Sword";
            case 228:
                return "Mythical Sword";
            case 229:
                return "Legend Sword";
            case 230:
                return "Highland Blade";
            case 231:
                return "Balrog Blade";
            case 232:
                return "Champion Sword";
            case 233:
                return "Colossal Sword";
            case 234:
                return "Colossus Blade";
            case 235:
                return "Bone Knife";
            case 236:
                return "Mithral Point";
            case 237:
                return "Fanged Knife";
            case 238:
                return "Legend Spike";
            case 239:
                return "Flying Knife";
            case 240:
                return "Flying Axe";
            case 241:
                return "Winged Knife";
            case 242:
                return "Winged Axe";
            case 243:
                return "Hyperion Javelin";
            case 244:
                return "Stygian Pilum";
            case 245:
                return "Balrog Spear";
            case 246:
                return "Ghost Glaive";
            case 247:
                return "Winged Harpoon";
            case 248:
                return "Hyperion Spear";
            case 249:
                return "Stygian Pike";
            case 250:
                return "Mancatcher";
            case 251:
                return "Ghost Spear";
            case 252:
                return "War Pike";
            case 253:
                return "Ogre Axe";
            case 254:
                return "Colossus Voulge";
            case 255:
                return "Thresher";
            case 256:
                return "Cryptic Axe";
            case 257:
                return "Great Poleaxe";
            case 258:
                return "Giant Thresher";
            case 259:
                return "Walking Stick";
            case 260:
                return "Stalagmite";
            case 261:
                return "Elder Staff";
            case 262:
                return "Shillelagh";
            case 263:
                return "Archon Staff";
            case 264:
                return "Spider Bow";
            case 265:
                return "Blade Bow";
            case 266:
                return "Shadow Bow";
            case 267:
                return "Great Bow";
            case 268:
                return "Diamond Bow";
            case 269:
                return "Crusader Bow";
            case 270:
                return "Ward Bow";
            case 271:
                return "Hydra Bow";
            case 272:
                return "Pellet Bow";
            case 273:
                return "Gorgon Crossbow";
            case 274:
                return "Colossus Crossbow";
            case 275:
                return "Demon Crossbow";
            case 276:
                return "Eagle Orb";
            case 277:
                return "Sacred Globe";
            case 278:
                return "Smoked Sphere";
            case 279:
                return "Clasped Orb";
            case 280:
                return "Jared's Stone";
            case 281:
                return "Stag Bow";
            case 282:
                return "Reflex Bow";
            case 283:
                return "Maiden Spear";
            case 284:
                return "Maiden Pike";
            case 285:
                return "Maiden Javelin";
            case 286:
                return "Glowing Orb";
            case 287:
                return "Crystalline Globe";
            case 288:
                return "Cloudy Sphere";
            case 289:
                return "Sparkling Ball";
            case 290:
                return "Swirling Crystal";
            case 291:
                return "Ashwood Bow";
            case 292:
                return "Ceremonial Bow";
            case 293:
                return "Ceremonial Spear";
            case 294:
                return "Ceremonial Pike";
            case 295:
                return "Ceremonial Javelin";
            case 296:
                return "Heavenly Stone";
            case 297:
                return "Eldritch Orb";
            case 298:
                return "Demon Heart";
            case 299:
                return "Vortex Orb";
            case 300:
                return "Dimensional Shard";
            case 301:
                return "Matriarchal Bow";
            case 302:
                return "Grand Matron Bow";
            case 303:
                return "Matriarchal Spear";
            case 304:
                return "Matriarchal Pike";
            case 305:
                return "Matriarchal Javelin";
            case 306:
                return "Cap";
            case 307:
                return "Skull Cap";
            case 308:
                return "Helm";
            case 309:
                return "Full Helm";
            case 310:
                return "Great Helm";
            case 311:
                return "Crown";
            case 312:
                return "Mask";
            case 313:
                return "Quilted Armor";
            case 314:
                return "Leather Armor";
            case 315:
                return "Hard Leather Armor";
            case 316:
                return "Studded Leather";
            case 317:
                return "Ring Mail";
            case 318:
                return "Scale Mail";
            case 319:
                return "Chain Mail";
            case 320:
                return "Breast Plate";
            case 321:
                return "Splint Mail";
            case 322:
                return "Plate Mail";
            case 323:
                return "Field Plate";
            case 324:
                return "Gothic Plate";
            case 325:
                return "Full Plate Mail";
            case 326:
                return "Ancient Armor";
            case 327:
                return "Light Plate";
            case 328:
                return "Buckler";
            case 329:
                return "Small Shield";
            case 330:
                return "Large Shield";
            case 331:
                return "Kite Shield";
            case 332:
                return "Tower Shield";
            case 333:
                return "Gothic Shield";
            case 334:
                return "Leather Gloves";
            case 335:
                return "Heavy Gloves";
            case 336:
                return "Chain Gloves";
            case 337:
                return "Light Gauntlets";
            case 338:
                return "Gauntlets";
            case 339:
                return "Boots";
            case 340:
                return "Heavy Boots";
            case 341:
                return "Chain Boots";
            case 342:
                return "Light Plated Boots";
            case 343:
                return "Greaves";
            case 344:
                return "Sash";
            case 345:
                return "Light Belt";
            case 346:
                return "Belt";
            case 347:
                return "Heavy Belt";
            case 348:
                return "Plated Belt";
            case 349:
                return "Bone Helm";
            case 350:
                return "Bone Shield";
            case 351:
                return "Spiked Shield";
            case 352:
                return "War Hat";
            case 353:
                return "Sallet";
            case 354:
                return "Casque";
            case 355:
                return "Basinet";
            case 356:
                return "Winged Helm";
            case 357:
                return "Grand Crown";
            case 358:
                return "Death Mask";
            case 359:
                return "Ghost Armor";
            case 360:
                return "Serpentskin Armor";
            case 361:
                return "Demonhide Armor";
            case 362:
                return "Trellised Armor";
            case 363:
                return "Linked Mail";
            case 364:
                return "Tigulated Mail";
            case 365:
                return "Mesh Armor";
            case 366:
                return "Cuirass";
            case 367:
                return "Russet Armor";
            case 368:
                return "Templar Coat";
            case 369:
                return "Sharktooth Armor";
            case 370:
                return "Embossed Plate";
            case 371:
                return "Chaos Armor";
            case 372:
                return "Ornate Plate";
            case 373:
                return "Mage Plate";
            case 374:
                return "Defender";
            case 375:
                return "Round Shield";
            case 376:
                return "Scutum";
            case 377:
                return "Dragon Shield";
            case 378:
                return "Pavise";
            case 379:
                return "Ancient Shield";
            case 380:
                return "Demonhide Gloves";
            case 381:
                return "Sharkskin Gloves";
            case 382:
                return "Heavy Bracers";
            case 383:
                return "Battle Gauntlets";
            case 384:
                return "War Gauntlets";
            case 385:
                return "Demonhide Boots";
            case 386:
                return "Sharkskin Boots";
            case 387:
                return "Mesh Boots";
            case 388:
                return "Battle Boots";
            case 389:
                return "War Boots";
            case 390:
                return "Demonhide Sash";
            case 391:
                return "Sharkskin Belt";
            case 392:
                return "Mesh Belt";
            case 393:
                return "Battle Belt";
            case 394:
                return "War Belt";
            case 395:
                return "Grim Helm";
            case 396:
                return "Grim Shield";
            case 397:
                return "Barbed Shield";
            case 398:
                return "Wolf Head";
            case 399:
                return "Hawk Helm";
            case 400:
                return "Antlers";
            case 401:
                return "Falcon Mask";
            case 402:
                return "Spirit Mask";
            case 403:
                return "Jawbone Cap";
            case 404:
                return "Fanged Helm";
            case 405:
                return "Horned Helm";
            case 406:
                return "Assault Helmet";
            case 407:
                return "Avenger Guard";
            case 408:
                return "Targe";
            case 409:
                return "Rondache";
            case 410:
                return "Heraldic Shield";
            case 411:
                return "Aerin Shield";
            case 412:
                return "Crown Shield";
            case 413:
                return "Preserved Head";
            case 414:
                return "Zombie Head";
            case 415:
                return "Unraveller Head";
            case 416:
                return "Gargoyle Head";
            case 417:
                return "Demon Head";
            case 418:
                return "Circlet";
            case 419:
                return "Coronet";
            case 420:
                return "Tiara";
            case 421:
                return "Diadem";
            case 422:
                return "Shako";
            case 423:
                return "Hydraskull";
            case 424:
                return "Armet";
            case 425:
                return "Giant Conch";
            case 426:
                return "Spired Helm";
            case 427:
                return "Corona";
            case 428:
                return "Demonhead";
            case 429:
                return "Dusk Shroud";
            case 430:
                return "Wyrmhide";
            case 431:
                return "Scarab Husk";
            case 432:
                return "Wire Fleece";
            case 433:
                return "Diamond Mail";
            case 434:
                return "Loricated Mail";
            case 435:
                return "Boneweave";
            case 436:
                return "Great Hauberk";
            case 437:
                return "Balrog Skin";
            case 438:
                return "Hellforge Plate";
            case 439:
                return "Kraken Shell";
            case 440:
                return "Lacquered Plate";
            case 441:
                return "Shadow Plate";
            case 442:
                return "Sacred Armor";
            case 443:
                return "Archon Plate";
            case 444:
                return "Heater";
            case 445:
                return "Luna";
            case 446:
                return "Hyperion";
            case 447:
                return "Monarch";
            case 448:
                return "Aegis";
            case 449:
                return "Ward";
            case 450:
                return "Bramble Mitts";
            case 451:
                return "Vampirebone Gloves";
            case 452:
                return "Vambraces";
            case 453:
                return "Crusader Gauntlets";
            case 454:
                return "Ogre Gauntlets";
            case 455:
                return "Wyrmhide Boots";
            case 456:
                return "Scarabshell Boots";
            case 457:
                return "Boneweave Boots";
            case 458:
                return "Mirrored Boots";
            case 459:
                return "Myrmidon Greaves";
            case 460:
                return "Spiderweb Sash";
            case 461:
                return "Vampirefang Belt";
            case 462:
                return "Mithril Coil";
            case 463:
                return "Troll Belt";
            case 464:
                return "Colossus Girdle";
            case 465:
                return "Bone Visage";
            case 466:
                return "Troll Nest";
            case 467:
                return "Blade Barrier";
            case 468:
                return "Alpha Helm";
            case 469:
                return "Griffon Headress";
            case 470:
                return "Hunter's Guise";
            case 471:
                return "Sacred Feathers";
            case 472:
                return "Totemic Mask";
            case 473:
                return "Jawbone Visor";
            case 474:
                return "Lion Helm";
            case 475:
                return "Rage Mask";
            case 476:
                return "Savage Helmet";
            case 477:
                return "Slayer Guard";
            case 478:
                return "Akaran Targe";
            case 479:
                return "Akaran Rondache";
            case 480:
                return "Protector Shield";
            case 481:
                return "Gilded Shield";
            case 482:
                return "Royal Shield";
            case 483:
                return "Mummified Trophy";
            case 484:
                return "Fetish Trophy";
            case 485:
                return "Sexton Trophy";
            case 486:
                return "Cantor Trophy";
            case 487:
                return "Hierophant Trophy";
            case 488:
                return "Blood Spirit";
            case 489:
                return "Sun Spirit";
            case 490:
                return "Earth Spirit";
            case 491:
                return "Sky Spirit";
            case 492:
                return "Dream Spirit";
            case 493:
                return "Carnage Helm";
            case 494:
                return "Fury Visor";
            case 495:
                return "Destroyer Helm";
            case 496:
                return "Conqueror Crown";
            case 497:
                return "Guardian Crown";
            case 498:
                return "Sacred Targe";
            case 499:
                return "Sacred Rondache";
            case 500:
                return "Kurast Shield";
            case 501:
                return "Zakarum Shield";
            case 502:
                return "Vortex Shield";
            case 503:
                return "Minion Skull";
            case 504:
                return "Hellspawn Skull";
            case 505:
                return "Overseer Skull";
            case 506:
                return "Succubus Skull";
            case 507:
                return "Bloodlord Skull";
            case 508:
                return "Elixir";
            case 509:
                return "Healing Potion";
            case 510:
                return "Mana Potion";
            case 511:
                return "Full Healing Potion";
            case 512:
                return "Full Mana Potion";
            case 513:
                return "Stamina Potion";
            case 514:
                return "Antidote Potion";
            case 515:
                return "Rejuvenation Potion";
            case 516:
                return "Full Rejuvenation Potion";
            case 517:
                return "Thawing Potion";
            case 518:
                return "Tome of Town Portal";
            case 519:
                return "Tome of Identify";
            case 520:
                return "Amulet";
            case 521:
                return "Amulet of the Viper";
            case 522:
                return "Ring";
            case 523:
                return "Gold";
            case 524:
                return "Scroll of Inifuss";
            case 525:
                return "Key to the Cairn Stones";
            case 526:
                return "Arrows";
            case 527:
                return "Torch";
            case 528:
                return "Bolts";
            case 529:
                return "Scroll of Town Portal";
            case 530:
                return "Scroll of Identify";
            case 531:
                return "Heart";
            case 532:
                return "Brain";
            case 533:
                return "Jawbone";
            case 534:
                return "Eye";
            case 535:
                return "Horn";
            case 536:
                return "Tail";
            case 537:
                return "Flag";
            case 538:
                return "Fang";
            case 539:
                return "Quill";
            case 540:
                return "Soul";
            case 541:
                return "Scalp";
            case 542:
                return "Spleen";
            case 543:
                return "Key";
            case 544:
                return "The Black Tower Key";
            case 546:
                return "A Jade Figurine";
            case 547:
                return "The Golden Bird";
            case 548:
                return "Lam Esen's Tome";
            case 549:
                return "Horadric Cube";
            case 550:
                return "Horadric Scroll";
            case 551:
                return "Mephisto's Soulstone";
            case 552:
                return "Book of Skill";
            case 553:
                return "Khalim's Eye";
            case 554:
                return "Khalim's Heart";
            case 555:
                return "Khalim's Brain";
            case 556:
                return "Ear";
            case 557:
                return "Chipped Amethyst";
            case 558:
                return "Flawed Amethyst";
            case 559:
                return "Amethyst";
            case 560:
                return "Flawless Amethyst";
            case 561:
                return "Perfect Amethyst";
            case 562:
                return "Chipped Topaz";
            case 563:
                return "Flawed Topaz";
            case 564:
                return "Topaz";
            case 565:
                return "Flawless Topaz";
            case 566:
                return "Perfect Topaz";
            case 567:
                return "Chipped Sapphire";
            case 568:
                return "Flawed Sapphire";
            case 569:
                return "Sapphire";
            case 570:
                return "Flawless Sapphire";
            case 571:
                return "Perfect Sapphire";
            case 572:
                return "Chipped Emerald";
            case 573:
                return "Flawed Emerald";
            case 574:
                return "Emerald";
            case 575:
                return "Flawless Emerald";
            case 576:
                return "Perfect Emerald";
            case 577:
                return "Chipped Ruby";
            case 578:
                return "Flawed Ruby";
            case 579:
                return "Ruby";
            case 580:
                return "Flawless Ruby";
            case 581:
                return "Perfect Ruby";
            case 582:
                return "Chipped Diamond";
            case 583:
                return "Flawed Diamond";
            case 584:
                return "Diamond";
            case 585:
                return "Flawless Diamond";
            case 586:
                return "Perfect Diamond";
            case 545:
                return "Potion of Life";
            case 587:
                return "Minor Healing Potion";
            case 588:
                return "Light Healing Potion";
            case 589:
                return "Healing Potion";
            case 590:
                return "Greater Healing Potion";
            case 591:
                return "Super Healing Potion";
            case 592:
                return "Minor Mana Potion";
            case 593:
                return "Light Mana Potion";
            case 594:
                return "Mana Potion";
            case 595:
                return "Greater Mana Potion";
            case 596:
                return "Super Mana Potion";
            case 597:
                return "Chipped Skull";
            case 598:
                return "Flawed Skull";
            case 599:
                return "Skull";
            case 600:
                return "Flawless Skull";
            case 601:
                return "Perfect Skull";
            case 602:
                return "Herb";
            case 603:
                return "Small Charm";
            case 604:
                return "Large Charm";
            case 605:
                return "Grand Charm";
            case 606:
                return "Small Red Potion";
            case 607:
                return "Large Red Potion";
            case 608:
                return "Small Blue Potion";
            case 609:
                return "Large Blue Potion";
            case 610:
                return "El Rune";
            case 611:
                return "Eld Rune";
            case 612:
                return "Tir Rune";
            case 613:
                return "Nef Rune";
            case 614:
                return "Eth Rune";
            case 615:
                return "Ith Rune";
            case 616:
                return "Tal Rune";
            case 617:
                return "Ral Rune";
            case 618:
                return "Ort Rune";
            case 619:
                return "Thul Rune";
            case 620:
                return "Amn Rune";
            case 621:
                return "Sol Rune";
            case 622:
                return "Shael Rune";
            case 623:
                return "Dol Rune";
            case 624:
                return "Hel Rune";
            case 625:
                return "Io Rune";
            case 626:
                return "Lum Rune";
            case 627:
                return "Ko Rune";
            case 628:
                return "Fal Rune";
            case 629:
                return "Lem Rune";
            case 630:
                return "Pul Rune";
            case 631:
                return "Um Rune";
            case 632:
                return "Mal Rune";
            case 633:
                return "Ist Rune";
            case 634:
                return "Gul Rune";
            case 635:
                return "Vex Rune";
            case 636:
                return "Ohm Rune";
            case 637:
                return "Lo Rune";
            case 638:
                return "Sur Rune";
            case 639:
                return "Ber Rune";
            case 640:
                return "Jah Rune";
            case 641:
                return "Cham Rune";
            case 642:
                return "Zod Rune";
            case 643:
                return "Jewel";
            case 644:
                return "Malah's Potion";
            case 645:
                return "Scroll of Knowledge";
            case 646:
                return "Scroll of Resistance";
            case 647:
                return "Key of Terror";
            case 648:
                return "Key of Hate";
            case 649:
                return "Key of Destruction";
            case 650:
                return "Diablo's Horn";
            case 651:
                return "Baal's Eye";
            case 652:
                return "Mephisto's Brain";
            case 653:
                return "Token Of Absolution";
            case 654:
                return "Twisted Essence Of Suffering";
            case 655:
                return "Charged Essence Of Hatred";
            case 656:
                return "Burning Essence Of Terror";
            case 657:
                return "Festering Essence Of Destruction";
            case 658:
                return "Standard Of Heroes";
            default:
                return "";
        }
    }
}
