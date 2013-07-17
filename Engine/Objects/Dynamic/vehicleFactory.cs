using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Battle_Tanks.Objects
{
    enum eTankType { NORMAL, AGILE, IMPROVED, HEAVY }
    /// <summary>
    /// struktura z podstawowymi wartosciami dla szybkosci czolgow/pocisków
    /// </summary>
    struct objSpeed
    {   // czołgi
        static public float SLOW = 1.0f;
        static public float DEFAULT = 1.5f;
        static public float IMPROVED = 1.7f;
        static public float QUICK = 2.8f;

        static public float BULLET_BASE = 2.8f;
        static public float BULLET_QUICK = 3.5f;
    }
    static class vehicleFactory
    {
        /// <summary>
        /// tworzenie przeciwników
        /// </summary>
        /// <param name="t">rodzaj czołgu</param>
        /// <param name="hp">ilosc życia</param>
        /// <param name="dmg">uszkodzenia</param>
        /// <param name="pos">punkt w którum ma się pojawić czołg</param>
        /// <returns></returns>
        static public Vehicle createEnemyTank(eTankType t, int hp, int dmg, Point pos)
        {
            float spd = objSpeed.DEFAULT;
            Weapon enemyWeapon;
            switch(t)
            {
                case eTankType.AGILE:   spd = objSpeed.QUICK;       break;
                case eTankType.NORMAL:  spd = objSpeed.DEFAULT;     break;
                case eTankType.IMPROVED:spd = objSpeed.IMPROVED;    break;
                case eTankType.HEAVY   :spd = objSpeed.SLOW;        break;
            }
            Enemy enemy = new Enemy(pos, spd, eDir.U, hp);
            enemy.setWeapon(enemyWeapon = new Weapon(enemy));
            enemyWeapon.damage = dmg;
            enemy.setVisuals(t);
            if (t == eTankType.AGILE) enemyWeapon.speed = objSpeed.BULLET_QUICK;
            if (t == eTankType.HEAVY || t == eTankType.IMPROVED) enemyWeapon.maxBullets = 2;
            return enemy;
        }
    }
}
