public class LevelScore {

    private static const Dictionary<int, string> scoreNames = new()
    {
        [-3] = "Jackpot",
        [-2] = "Ace In The Hole",
        [-1] = "Birdie",
        [0] = "Break Even",
        [1] = "Bogey",
        [2] = "Snake Eyes",
        [3] = "Down To The Wire",
        [4] = "No Dice",
    };

    private static const int minScore = -3;
    private static const int maxScore = 4;
    
    public static string Result(int score, int par) {
        int diff = score - par;
        if (diff > maxScore) {
            diff = maxScore;
        } if (diff < minScore) {
            diff = minScore;
        }

        return  scoreNames[diff];
    }
}