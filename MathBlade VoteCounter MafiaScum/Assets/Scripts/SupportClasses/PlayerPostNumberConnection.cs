using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public abstract class PlayerPostNumberConnection
    {
        public PlayerPostNumberConnection(Player player, int postnumber)
        {
            Player = player;
            PostNumber = postnumber;
        }

        public Player Player { get; set; }
        public int PostNumber { get; set; }
    }
}
