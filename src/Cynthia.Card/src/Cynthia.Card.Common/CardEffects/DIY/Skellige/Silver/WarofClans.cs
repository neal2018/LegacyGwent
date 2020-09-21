using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("70057")]//群岛战争 WarofClans
    public class WarofClans : CardEffect
    {//己方场上每有1种家族标签，便对全场造成1次1点伤害。
        public WarofClans(GameCard card) : base(card) { }
        public override async Task<int> CardUseEffect()
        {
            int[] clan = new int[7]{0,0,0,0,0,0,0};
            int num = 0;
            var clan_cards = Game.GetAllCard(Card.PlayerIndex).Where(x => x.Status.CardRow.IsOnPlace() && x.PlayerIndex == Card.PlayerIndex).Mess(RNG).ToList();
            foreach (var card in clan_cards) //统计家族数
            {
                if (card.HasAllCategorie(Categorie.ClanDrummond) && clan[0] == 0)// "德拉蒙家族" 
                {
                    clan[0] =1;
                    num++;
                }
                else if(card.HasAllCategorie(Categorie.ClanTuirseach) && clan[1] == 0)// "图尔赛克家族" 
                {
                    clan[1] =1;
                    num++;
                }
                else if(card.HasAllCategorie(Categorie.ClanTordarroch) && clan[2] == 0)// "托达洛克家族" 
                {
                    clan[2] =1;
                    num++;
                }
                else if(card.HasAllCategorie(Categorie.ClanDimun) && clan[3] == 0)// "迪门家族" 
                {
                    clan[3] =1;
                    num++;
                }
                else if(card.HasAllCategorie(Categorie.ClanHeymaey) && clan[4] == 0)// "海玫家族" 
                {
                    clan[4] =1;
                    num++;
                }
                else if(card.HasAllCategorie(Categorie.ClanAnCraite) && clan[5] == 0) // "奎特家族" 
                {
                    clan[5] =1;
                    num++;
                }
                else if(card.HasAllCategorie(Categorie.ClanBrokvar ) && clan[6] == 0)//"布洛克瓦尔家族" 
                {
                    clan[6] =1;
                    num++;
                }
            }
            // 造成伤害
            while(num > 0)
            {
                num--;
                var cards = Game.GetAllCard(Card.PlayerIndex).Where(x => x.Status.CardRow.IsOnPlace()).Mess(RNG).ToList();
                foreach (var card in cards)
                {
                    if (card.Status.CardRow.IsOnPlace())
                        await card.Effect.Damage(1, Card);
                }
            }
            return 0;
        }
    }
}