using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SkillsStruc
{
    Form1 Form1_0;

    public string LeftSkillName = "";
    public string RightSkillName = "";
    public int LeftSkill = 0;
    public int RightSkill = 0;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void SetSkill(string Direction, int skill)
    {
        if (Direction == "Left")
        {
            LeftSkill = skill;
            LeftSkillName = getSkillName(skill);
        }
        if (Direction == "Right")
        {
            RightSkill = skill;
            RightSkillName = getSkillName(skill);
        }
    }

    public string getSkillName(int skill)
    {
        switch (skill)
        {
            case 0:
                return "attack";
            case 1:
                return "kick";
            case 2:
                return "throw";
            case 3:
                return "unsummon";
            case 4:
                return "left hand throw";
            case 5:
                return "left hand swing";
            case 6:
                return "magic arrow";
            case 7:
                return "fire arrow";
            case 8:
                return "inner sight";
            case 9:
                return "critical strike";
            case 10:
                return "jab";
            case 11:
                return "cold arrow";
            case 12:
                return "multiple shot";
            case 13:
                return "dodge";
            case 14:
                return "power strike";
            case 15:
                return "poison javelin";
            case 16:
                return "exploding arrow";
            case 17:
                return "slow missiles";
            case 18:
                return "avoid";
            case 19:
                return "impale";
            case 20:
                return "lightning bolt";
            case 21:
                return "ice arrow";
            case 22:
                return "guided arrow";
            case 23:
                return "penetrate";
            case 24:
                return "charged strike";
            case 25:
                return "plague javelin";
            case 26:
                return "strafe";
            case 27:
                return "immolation arrow";
            case 28:
                return "dopplezon";
            case 29:
                return "evade";
            case 30:
                return "fend";
            case 31:
                return "freezing arrow";
            case 32:
                return "valkyrie";
            case 33:
                return "pierce";
            case 34:
                return "lightning strike";
            case 35:
                return "lightning fury";
            case 36:
                return "fire bolt";
            case 37:
                return "warmth";
            case 38:
                return "charged bolt";
            case 39:
                return "ice bolt";
            case 40:
                return "frozen armor";
            case 41:
                return "inferno";
            case 42:
                return "static field";
            case 43:
                return "telekinesis";
            case 44:
                return "frost nova";
            case 45:
                return "ice blast";
            case 46:
                return "blaze";
            case 47:
                return "fire ball";
            case 48:
                return "nova";
            case 49:
                return "lightning";
            case 50:
                return "shiver armor";
            case 51:
                return "fire wall";
            case 52:
                return "enchant";
            case 53:
                return "chain lightning";
            case 54:
                return "teleport";
            case 55:
                return "glacial spike";
            case 56:
                return "meteor";
            case 57:
                return "thunder storm";
            case 58:
                return "energy shield";
            case 59:
                return "blizzard";
            case 60:
                return "chilling armor";
            case 61:
                return "fire mastery";
            case 62:
                return "hydra";
            case 63:
                return "lightning mastery";
            case 64:
                return "frozen orb";
            case 65:
                return "cold mastery";
            case 66:
                return "amplify damage";
            case 67:
                return "teeth";
            case 68:
                return "bone armor";
            case 69:
                return "skeleton mastery";
            case 70:
                return "raise skeleton";
            case 71:
                return "dim vision";
            case 72:
                return "weaken";
            case 73:
                return "poison dagger";
            case 74:
                return "corpse explosion";
            case 75:
                return "clay golem";
            case 76:
                return "iron maiden";
            case 77:
                return "terror";
            case 78:
                return "bone wall";
            case 79:
                return "golem mastery";
            case 80:
                return "raise skeletal mage";
            case 81:
                return "confuse";
            case 82:
                return "life tap";
            case 83:
                return "poison explosion";
            case 84:
                return "bone spear";
            case 85:
                return "bloodgolem";
            case 86:
                return "attract";
            case 87:
                return "decrepify";
            case 88:
                return "bone prison";
            case 89:
                return "summon resist";
            case 90:
                return "irongolem";
            case 91:
                return "lower resist";
            case 92:
                return "poison nova";
            case 93:
                return "bone spirit";
            case 94:
                return "firegolem";
            case 95:
                return "revive";
            case 96:
                return "sacrifice";
            case 97:
                return "smite";
            case 98:
                return "might";
            case 99:
                return "prayer";
            case 100:
                return "resist fire";
            case 101:
                return "holy bolt";
            case 102:
                return "holy fire";
            case 103:
                return "thorns";
            case 104:
                return "defiance";
            case 105:
                return "resist cold";
            case 106:
                return "zeal";
            case 107:
                return "charge";
            case 108:
                return "blessed aim";
            case 109:
                return "cleansing";
            case 110:
                return "resist lightning";
            case 111:
                return "vengeance";
            case 112:
                return "blessed hammer";
            case 113:
                return "concentration";
            case 114:
                return "holy freeze";
            case 115:
                return "vigor";
            case 116:
                return "conversion";
            case 117:
                return "holy shield";
            case 118:
                return "holy shock";
            case 119:
                return "sanctuary";
            case 120:
                return "meditation";
            case 121:
                return "fist of the heavens";
            case 122:
                return "fanaticism";
            case 123:
                return "conviction";
            case 124:
                return "redemption";
            case 125:
                return "salvation";
            case 126:
                return "bash";
            case 127:
                return "blade mastery";
            case 128:
                return "axe mastery";
            case 129:
                return "mace mastery";
            case 130:
                return "howl";
            case 131:
                return "find potion";
            case 132:
                return "leap";
            case 133:
                return "double swing";
            case 134:
                return "pole arm mastery";
            case 135:
                return "throwing mastery";
            case 136:
                return "spear mastery";
            case 137:
                return "taunt";
            case 138:
                return "shout";
            case 139:
                return "stun";
            case 140:
                return "double throw";
            case 141:
                return "increased stamina";
            case 142:
                return "find item";
            case 143:
                return "leap attack";
            case 144:
                return "concentrate";
            case 145:
                return "iron skin";
            case 146:
                return "battle cry";
            case 147:
                return "frenzy";
            case 148:
                return "increased speed";
            case 149:
                return "battle orders";
            case 150:
                return "grim ward";
            case 151:
                return "whirlwind";
            case 152:
                return "berserk";
            case 153:
                return "natural resistance";
            case 154:
                return "war cry";
            case 155:
                return "battle command";
            case 197:
                return "firestorm";
            case 217:
                return "scroll of identify";
            case 218:
                return "book of identify";
            case 219:
                return "scroll of townportal";
            case 220:
                return "book of townportal";
            case 221:
                return "raven";
            case 222:
                return "plague poppy";
            case 223:
                return "wearwolf";
            case 224:
                return "shape shifting";
            case 225:
                return "firestorm";
            case 226:
                return "oak sage";
            case 227:
                return "summon spirit wolf";
            case 228:
                return "wearbear";
            case 229:
                return "molten boulder";
            case 230:
                return "arctic blast";
            case 231:
                return "cycle of life";
            case 232:
                return "feral rage";
            case 233:
                return "maul";
            case 234:
                return "eruption";
            case 235:
                return "cyclone armor";
            case 236:
                return "heart of wolverine";
            case 237:
                return "summon fenris";
            case 238:
                return "rabies";
            case 239:
                return "fire claws";
            case 240:
                return "twister";
            case 241:
                return "vines";
            case 242:
                return "hunger";
            case 243:
                return "shock wave";
            case 244:
                return "volcano";
            case 245:
                return "tornado";
            case 246:
                return "spirit of barbs";
            case 247:
                return "summon grizzly";
            case 248:
                return "fury";
            case 249:
                return "armageddon";
            case 250:
                return "hurricane";
            case 251:
                return "fire trauma";
            case 252:
                return "claw mastery";
            case 253:
                return "psychic hammer";
            case 254:
                return "tiger strike";
            case 255:
                return "dragon talon";
            case 256:
                return "shock field";
            case 257:
                return "blade sentinel";
            case 258:
                return "quickness";
            case 259:
                return "fists of fire";
            case 260:
                return "dragon claw";
            case 261:
                return "charged bolt sentry";
            case 262:
                return "wake of fire sentry";
            case 263:
                return "weapon block";
            case 264:
                return "cloak of shadows";
            case 265:
                return "cobra strike";
            case 266:
                return "blade fury";
            case 267:
                return "fade";
            case 268:
                return "shadow warrior";
            case 269:
                return "claws of thunder";
            case 270:
                return "dragon tail";
            case 271:
                return "lightning sentry";
            case 272:
                return "inferno sentry";
            case 273:
                return "mind blast";
            case 274:
                return "blades of ice";
            case 275:
                return "dragon flight";
            case 276:
                return "death sentry";
            case 277:
                return "blade shield";
            case 278:
                return "venom";
            case 279:
                return "shadow master";
            case 280:
                return "royal strike";
            case 350:
                return "delerium change";
            case 357:
                return "interact";
            case 358:
                return "loot";
            case 359:
                return "townportal";
            case 360:
                return "emotewheel";
            case 361:
                return "swapweapons";
            case 362:
                return "map";
            case 363:
                return "showitems";
            case 364:
                return "runtoggle";
        }
        return "";
    }

    public string getSkillClass(int skill)
    {
        if (skill >= 6 && skill <= 35)
        {
            return "Amazon";
        }
        if (skill >= 36 && skill <= 65)
        {
            return "Sorceress";
        }
        if (skill >= 66 && skill <= 95)
        {
            return "Necromancer";
        }
        if (skill >= 126 && skill <= 155)
        {
            return "Barbarian";
        }
        return "";
    }


    public string getSkillTree(int skilltree)
    {
        switch (skilltree)
        {
            case 0:
                return "bow and crossbow";
            case 1:
                return "passive and magic";
            case 2:
                return "javelin and spear";
            case 8:
                return "fire";
            case 9:
                return "lightning";
            case 10:
                return "cold";
            case 16:
                return "curses";
            case 17:
                return "poison and bone";
            case 18:
                return "summoning";
            case 24:
                return "combat skills";
            case 25:
                return "offensivea auras";
            case 26:
                return "defensive auras";
            case 32:
                return "combats kills";
            case 33:
                return "masteries";
            case 34:
                return "warcries";
            case 40:
                return "summoning";
            case 41:
                return "shape shifting";
            case 42:
                return "elemental";
            case 48:
                return "traps";
            case 49:
                return "shadow disciplines";
            case 50:
                return "martial arts";
        }
        return "";
    }
}
