using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class DayviggedPlayer : PlayerPostNumberConnection
    {
        public DayviggedPlayer(Player player, int postnumber) : base(player, postnumber)
        {
        }
    }
}
