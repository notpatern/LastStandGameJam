
namespace Scripts {
    static public class CurrencyManager {
        static public int playerMoney = 0;

        static public void playerGetMoney(int amount) {
            playerMoney += amount;
        }

        static public void playerRemoveMoney(int amount) {
            playerMoney -= amount;
        }
    }
}
