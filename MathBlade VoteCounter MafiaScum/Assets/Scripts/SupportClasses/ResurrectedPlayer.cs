using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class ResurrectedPlayer : PlayerPostNumberConnection
    {
        public ResurrectedPlayer(Player player, int postNumber) : base(player, postNumber)
        {
        }
    }
}
