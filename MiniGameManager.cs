using System;
using System.Collections.Generic;

namespace ProgPart2
{
    public class MiniGameManager
    {
        private readonly Random rand = new Random();
        private int currentScore = 0;
        private int currentQuestionIndex = 0;
        private bool isGameActive = false;

        private List<QuizQuestion> questions;

        public MiniGameManager()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion { Question = "What should you do if you receive an email asking for your password?",
                    Options = new[] { "A) Reply with your password", "B) Delete the email", "C) Report as phishing", "D) Ignore it" },
                    CorrectAnswer = "C", Explanation = "Correct! Reporting phishing helps prevent scams." },

                new QuizQuestion { Question = "Which of these is the strongest password?",
                    Options = new[] { "A) Password123", "B) MyDog2023", "C) P@ssw0rd!", "D) Tr0ub4dor&3x4mpl3!" },
                    CorrectAnswer = "D", Explanation = "Long complex passwords with symbols are much stronger." },

                new QuizQuestion { Question = "True or False: You should use the same password on multiple accounts.",
                    Options = new[] { "A) True", "B) False" }, CorrectAnswer = "B",
                    Explanation = "False. Password reuse is very dangerous." },

                new QuizQuestion { Question = "What does 2FA stand for?",
                    Options = new[] { "A) Two Factor Authentication", "B) Two Firewall Access", "C) Too Fast Access", "D) Two Factor Access" },
                    CorrectAnswer = "A", Explanation = "Two-Factor Authentication adds an extra security layer." },

                new QuizQuestion { Question = "What is the best way to protect yourself on public Wi-Fi?",
                    Options = new[] { "A) Use a VPN", "B) Turn off Wi-Fi", "C) Use any website", "D) Share your location" },
                    CorrectAnswer = "A", Explanation = "A VPN encrypts your connection." },

                new QuizQuestion { Question = "What is phishing?",
                    Options = new[] { "A) A type of fish", "B) Fake emails/websites to steal info", "C) A computer virus", "D) A firewall" },
                    CorrectAnswer = "B", Explanation = "Phishing tricks users into giving sensitive information." },

                new QuizQuestion { Question = "True or False: Regular software updates help with security.",
                    Options = new[] { "A) True", "B) False" }, CorrectAnswer = "A",
                    Explanation = "True. Updates patch security vulnerabilities." },

                new QuizQuestion { Question = "What is ransomware?",
                    Options = new[] { "A) A backup tool", "B) Software that locks your files for ransom", "C) A strong password", "D) Antivirus software" },
                    CorrectAnswer = "B", Explanation = "Ransomware encrypts files and demands payment." },

                new QuizQuestion { Question = "What does VPN stand for?",
                    Options = new[] { "A) Virtual Private Network", "B) Very Private Network", "C) Virus Protection Network", "D) Visual Private Network" },
                    CorrectAnswer = "A", Explanation = "A VPN protects your internet traffic." },

                new QuizQuestion { Question = "Which is the safest way to store passwords?",
                    Options = new[] { "A) Write them on paper", "B) Use a password manager", "C) Save them in browser", "D) Use the same one everywhere" },
                    CorrectAnswer = "B", Explanation = "Password managers are the safest option." }
            };
        }

        public string StartGame()
        {
            isGameActive = true;
            currentScore = 0;
            currentQuestionIndex = 0;
            return "🧠 **Cybersecurity Mini Game Started!**\n\n" + GetCurrentQuestion();
        }

        public string ProcessAnswer(string answer)
        {
            if (!isGameActive)
                return "No game is currently active. Type 'start quiz' to begin.";

            string upperAnswer = answer.Trim().ToUpper();

            if (currentQuestionIndex >= questions.Count)
                return EndGame();

            var currentQ = questions[currentQuestionIndex];
            bool isCorrect = upperAnswer.StartsWith(currentQ.CorrectAnswer);

            if (isCorrect)
            {
                currentScore++;
                currentQuestionIndex++;
                return $"✅ **Correct!** {currentQ.Explanation}\n\n" + GetCurrentQuestion();
            }
            else
            {
                currentQuestionIndex++;
                return $"❌ Wrong. The correct answer was **{currentQ.CorrectAnswer}**.\n{currentQ.Explanation}\n\n" + GetCurrentQuestion();
            }
        }

        private string GetCurrentQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
                return EndGame();

            var q = questions[currentQuestionIndex];
            string options = string.Join("\n", q.Options);
            return $"**Question {currentQuestionIndex + 1}/{questions.Count}:**\n{q.Question}\n\n{options}\n\nReply with the letter (A, B, C, or D).";
        }

        public string EndGame()
        {
            isGameActive = false;
            double percentage = questions.Count > 0 ? (double)currentScore / questions.Count * 100 : 0;

            string feedback;

            if (percentage >= 90)
                feedback = "Outstanding! You're a cybersecurity expert!";
            else if (percentage >= 70)
                feedback = "Great job! You're well on your way.";
            else if (percentage >= 50)
                feedback = "Good effort! Keep learning.";
            else
                feedback = "Keep practicing! Cybersecurity knowledge improves with time.";

            return $"**Mini Game Complete!**\n\n" +
                   $"**Score:** {currentScore}/{questions.Count} ({percentage:F0}%)\n\n" +
                   feedback;
        }

        public bool IsGameActive() => isGameActive;
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }
    }
}