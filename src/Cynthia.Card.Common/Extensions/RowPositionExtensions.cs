namespace Cynthia.Card
{
    public static class RowPositionExtensions
    {
        public static bool IsOnPlace(this RowPosition row)
        {
            switch (row)
            {
                case RowPosition.MyRow1:
                    return true;
                case RowPosition.MyRow2:
                    return true;
                case RowPosition.MyRow3:
                    return true;
                case RowPosition.EnemyRow1:
                    return true;
                case RowPosition.EnemyRow2:
                    return true;
                case RowPosition.EnemyRow3:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsInBack(this RowPosition row)
        {
            switch (row)
            {
                case RowPosition.MyCemetery:
                    return true;
                case RowPosition.MyDeck:
                    return true;
                case RowPosition.EnemyCemetery:
                    return true;
                case RowPosition.EnemyDeck:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsMyRow(this RowPosition row)
        {
            switch (row)
            {
                case RowPosition.MyRow1:
                    return true;
                case RowPosition.MyRow2:
                    return true;
                case RowPosition.MyRow3:
                    return true;
                case RowPosition.MyHand:
                    return true;
                case RowPosition.MyStay:
                    return true;
                case RowPosition.MyDeck:
                    return true;
                case RowPosition.MyCemetery:
                    return true;
                case RowPosition.SpecialPlace:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsOnRow(this RowPosition row)
        {
            switch (row)
            {
                case RowPosition.MyRow1:
                    return true;
                case RowPosition.MyRow2:
                    return true;
                case RowPosition.MyRow3:
                    return true;
                case RowPosition.MyHand:
                    return true;
                case RowPosition.MyStay:
                    return true;
                case RowPosition.EnemyRow1:
                    return true;
                case RowPosition.EnemyRow2:
                    return true;
                case RowPosition.EnemyRow3:
                    return true;
                case RowPosition.EnemyHand:
                    return true;
                case RowPosition.EnemyStay:
                    return true;
                default:
                    return false;
            }
        }
        public static RowPosition RowMirror(this RowPosition row)
        {
            switch (row)
            {
                case RowPosition.MyRow1:
                    return RowPosition.EnemyRow1;
                case RowPosition.MyRow2:
                    return RowPosition.EnemyRow2;
                case RowPosition.MyRow3:
                    return RowPosition.EnemyRow3;
                case RowPosition.EnemyRow1:
                    return RowPosition.MyRow1;
                case RowPosition.EnemyRow2:
                    return RowPosition.MyRow2;
                case RowPosition.EnemyRow3:
                    return RowPosition.MyRow3;
                case RowPosition.MyHand:
                    return RowPosition.EnemyHand;
                case RowPosition.EnemyHand:
                    return RowPosition.MyHand;
                case RowPosition.MyStay:
                    return RowPosition.EnemyStay;
                case RowPosition.EnemyStay:
                    return RowPosition.MyStay;
                case RowPosition.MyDeck:
                    return RowPosition.EnemyDeck;
                case RowPosition.EnemyDeck:
                    return RowPosition.MyDeck;
                case RowPosition.MyCemetery:
                    return RowPosition.EnemyCemetery;
                case RowPosition.EnemyCemetery:
                    return RowPosition.MyCemetery;
                case RowPosition.SpecialPlace:
                    return RowPosition.SpecialPlace;
                case RowPosition.MyLeader:
                    return RowPosition.EnemyLeader;
                case RowPosition.EnemyLeader:
                    return RowPosition.MyLeader;
            }
            return RowPosition.SpecialPlace;
        }
    }
}