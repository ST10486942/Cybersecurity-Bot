using System;
using System.Collections.Generic;

namespace ProgPart2
{
    public class ChatbotEngine
    {
        private readonly Random rand = new Random();
        private string userName = "Friend";

        private readonly TaskManager taskManager;
        private readonly MiniGameManager miniGameManager;
        private readonly ActivityLogger activityLogger;   // ← Added for Task 4

        private readonly Dictionary<string, string[]> responses;

        public ChatbotEngine(TaskManager taskManager, MiniGameManager miniGameManager, ActivityLogger activityLogger)
        {
            this.taskManager = taskManager;
            this.miniGameManager = miniGameManager;
            this.activityLogger = activityLogger;

            responses = new Dictionary<string, string[]>
            {
                { "password", new[]
                    {
                        "Use strong, unique passwords with at least 12 characters. Mix uppercase, lowercase, numbers, and symbols for maximum security.",
                        "Never reuse the same password across different accounts. If one gets compromised, all are at risk.",
                        "I highly recommend using a password manager like Bitwarden, LastPass, or 1Password.",
                        "Enable password autofill only on trusted devices and always combine it with 2FA."
                    }},

                { "phishing", new[]
                    {
                        "Phishing is a common attack where scammers pretend to be trusted organizations to steal your information.",
                        "Always check the URL carefully before entering any login details. Look for spelling mistakes.",
                        "Never click on links in suspicious emails. Hover over them first to see the real destination.",
                        "Be extra careful with emails that create urgency or threaten your account."
                    }},

                { "scam", new[]
                    {
                        "Scams often use fear or greed to push you into quick decisions. Take your time and verify.",
                        "If someone asks for money or personal details unexpectedly, it's likely a scam.",
                        "Government agencies will never ask you to pay via gift cards or cryptocurrency.",
                        "When in doubt, contact the organization directly using official contact details."
                    }},

                { "privacy", new[]
                    {
                        "Review app permissions regularly and revoke access for apps you no longer use.",
                        "Enable two-factor authentication (2FA) on all important accounts whenever possible.",
                        "Be mindful of what personal information you share on social media.",
                        "Use privacy-focused browsers and search engines when possible."
                    }},

                { "malware", new[]
                    {
                        "Malware is malicious software that can steal data, damage files, or spy on you.",
                        "Keep your operating system and antivirus software always up to date.",
                        "Avoid downloading files or software from untrusted websites.",
                        "Never open email attachments from unknown senders."
                    }},

                { "ransomware", new[]
                    {
                        "Ransomware encrypts your files and demands payment to unlock them.",
                        "The best defense against ransomware is making regular backups to an external drive or cloud.",
                        "Never pay the ransom — it only encourages more attacks.",
                        "Use reliable antivirus with ransomware protection features."
                    }},

                { "vpn", new[]
                    {
                        "A VPN encrypts your internet connection, making it much safer on public Wi-Fi.",
                        "Always use a reputable VPN when accessing sensitive data outside your home.",
                        "Good VPNs hide your IP address and protect you from eavesdroppers.",
                        "I recommend ExpressVPN, NordVPN, or ProtonVPN for strong privacy."
                    }},

                { "firewall", new[]
                    {
                        "A firewall acts as a barrier between your device and potential threats from the internet.",
                        "Make sure both Windows Firewall and your router's firewall are enabled.",
                        "Firewalls help block unauthorized access to your network.",
                        "Don't disable your firewall unless you have a very good reason."
                    }},

                { "antivirus", new[]
                    {
                        "Keep your antivirus software updated and run full scans regularly.",
                        "Windows Defender is actually quite good if kept updated.",
                        "Consider premium options like Bitdefender or Malwarebytes for extra protection.",
                        "Antivirus is most effective when combined with safe browsing habits."
                    }},

                { "hello", new[]
                    {
                        "Hello! Great to see you. How can I help you stay safe online today? 🛡️",
                        "Hi there! Ready to talk cybersecurity? What’s on your mind?",
                        "Hey! I'm here to help you with passwords, phishing, or any security questions.",
                        "Welcome back! What cybersecurity topic would you like to learn about?"
                    }}
                ,
                // === NEW 10 KEYWORDS (Add these inside the responses dictionary) ===

{ "social engineering", new[]
    {
        "Social engineering is when attackers manipulate people into revealing confidential information.",
        "Be cautious of unsolicited calls or messages asking for personal or account details.",
        "Verify the identity of anyone requesting sensitive information through official channels.",
        "Training yourself to recognize social engineering is one of the best defenses."
    }},

{ "encryption", new[]
    {
        "Encryption converts your data into a code that only authorized people can read.",
        "Always use HTTPS websites (look for the padlock icon) when entering personal data.",
        "Full disk encryption like BitLocker protects your files if your device is lost or stolen.",
        "End-to-end encrypted messaging apps like Signal provide strong privacy."
    }},

{ "backup", new[]
    {
        "Regular backups are your safety net against ransomware and hardware failure.",
        "Follow the 3-2-1 rule: 3 copies of data, on 2 different types of media, with 1 offsite.",
        "Automate your backups and test them occasionally to ensure they work.",
        "Use encrypted cloud storage or external drives for important files."
    }},

{ "update", new[]
    {
        "Keeping your software and operating system updated is crucial for security.",
        "Updates often patch critical vulnerabilities that hackers could exploit.",
        "Enable automatic updates for Windows, browsers, and important apps.",
        "Outdated software is one of the most common entry points for cyberattacks."
    }},

{ "public wifi", new[]
    {
        "Public Wi-Fi networks are often unsecured and easy for attackers to monitor.",
        "Avoid accessing banking or sensitive accounts when connected to public Wi-Fi.",
        "Always use a VPN when using public wireless networks.",
        "Forget the network after use and turn off auto-connect."
    }},

{ "identity theft", new[]
    {
        "Identity theft occurs when someone uses your personal information without permission.",
        "Monitor your bank accounts and credit reports regularly for suspicious activity.",
        "Freeze your credit if you suspect your information has been compromised.",
        "Never share sensitive documents or ID numbers unless absolutely necessary."
    }},

{ "safe browsing", new[]
    {
        "Safe browsing means being cautious about which websites you visit and what you download.",
        "Look for the HTTPS padlock and avoid websites with suspicious pop-ups.",
        "Use ad blockers and anti-tracking extensions for better protection.",
        "Keep your browser and plugins updated to the latest versions."
    }},

{ "two factor", new[]
    {
        "Two-factor authentication (2FA) greatly reduces the risk of account takeover.",
        "Use authenticator apps instead of SMS for 2FA when possible.",
        "Enable 2FA on your email, banking, and social media accounts immediately.",
        "Even strong passwords become much more secure when combined with 2FA."
    }},

{ "strong password", new[]
    {
        "Strong passwords should be long (12+ characters) and contain a mix of characters.",
        "Passphrases like 'MyFavoriteColorIsBlue2025!' are both secure and memorable.",
        "Avoid using common words, birthdays, or names in your passwords.",
        "A password manager is the best way to create and store strong passwords."
    }},

{ "phishing email", new[]
    {
        "Phishing emails often look professional but contain subtle red flags.",
        "Check the sender address carefully — many phishing emails use spoofed addresses.",
        "Urgent language like 'Your account will be suspended!' is a common tactic.",
        "When suspicious, contact the company directly using known official contact details."
    }},

                { "hi", new[] { "Hello! Great to see you...", "Hi there! Ready to talk...", /* repeat similar */ }},
                { "hey", new[] { "Hey! How can I help you stay safe today?" }}
            };
        }

        public void SetUserName(string name)
        {
            userName = string.IsNullOrWhiteSpace(name) ? "Friend" : name.Trim();
        }

        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "I didn't catch that. Could you please rephrase?";

            string lower = input.ToLower().Trim();

            activityLogger.LogAction("User Input", input);


            // ==========================
            // QUIZ MANAGEMENT
            // ==========================

            if (lower.Contains("exit quiz") ||
                lower.Contains("stop quiz") ||
                lower.Contains("quit quiz"))
            {
                activityLogger.LogAction("Quiz Stopped", "User exited quiz");
                miniGameManager.EndGame();

                return "🛑 Quiz stopped. You can start again anytime by typing 'start quiz'.";
            }


            if (miniGameManager.IsGameActive())
            {
                return miniGameManager.ProcessAnswer(input);
            }



            // ==========================
            // ACTIVITY LOG
            // ==========================

            if (lower.Contains("activity log") ||
                lower.Contains("show log") ||
                lower.Contains("history"))
            {
                activityLogger.LogAction(
                    "Activity Log Viewed",
                    "User requested logs");


                var logs = activityLogger.GetRecentLogs(10);

                return "📜 **Recent Activity Log:**\n\n" +
                       string.Join("\n", logs);
            }



            // ==========================
            // ADD TASK
            // ==========================

            if (lower.Contains("add task") ||
                lower.Contains("new task") ||
                lower.Contains("remind me"))
            {

                string title = ExtractTaskTitle(input);

                string reminder = ExtractReminder(input);


                if (string.IsNullOrWhiteSpace(title))
                    title = "General Cybersecurity Task";


                bool success = taskManager.AddTask(
                    userName,
                    title,
                    "Created via chatbot",
                    reminder);



                if (success)
                {
                    activityLogger.LogAction(
                        "Task Added",
                        title);


                    if (reminder != null)
                    {
                        return $"✅ Task added: **{title}**\n⏰ Reminder: {reminder}";
                    }


                    return $"✅ Task added: **{title}**";
                }


                return "❌ Failed to save task.";
            }




            // ==========================
            // SHOW TASKS
            // ==========================

            if (lower.Contains("show tasks") ||
               lower.Contains("my tasks") ||
               lower.Contains("view tasks") ||
               lower.Contains("pending tasks"))
            {

                var tasks = taskManager.GetAllTasks(userName);


                if (tasks.Count == 0)
                    return "You have no tasks yet.";


                string result = "📋 **Your Tasks:**\n\n";


                foreach (string task in tasks)
                {
                    result += task + "\n";
                }


                return result;
            }





            // ==========================
            // COMPLETE TASK
            // ==========================

            if (lower.Contains("complete task") ||
               lower.Contains("finish task"))
            {

                var numbers =
                System.Text.RegularExpressions.Regex
                .Matches(lower, @"\d+");


                if (numbers.Count > 0)
                {

                    int id = int.Parse(numbers[0].Value);


                    bool completed =
                        taskManager.CompleteTask(id);



                    if (completed)
                    {
                        activityLogger.LogAction(
                            "Task Completed",
                            id.ToString());


                        return $"✅ Task {id} marked as completed.";
                    }

                }


                return "Please specify the task number. Example: complete task 3";
            }





            // ==========================
            // START QUIZ
            // ==========================

            if (lower.Contains("start quiz") ||
               lower.Contains("play quiz") ||
               lower.Contains("mini game"))
            {

                activityLogger.LogAction(
                    "Quiz Started",
                    "User started quiz");


                return miniGameManager.StartGame();
            }




            // ==========================
            // NORMAL RESPONSES
            // ==========================

            string sentimentPrefix =
                GetSentimentPrefix(lower);


            foreach (var item in responses)
            {

                if (lower.Contains(item.Key))
                {

                    string response =
                        item.Value[rand.Next(item.Value.Length)];


                    activityLogger.LogAction(
                        "Topic Response",
                        item.Key);



                    return sentimentPrefix + response;
                }
            }



            activityLogger.LogAction(
                "Unknown Input",
                input);



            return sentimentPrefix +
            "I'm not sure I understood. Try passwords, phishing, add task, start quiz, or show tasks.";
        }






        // ==========================
        // TASK TITLE EXTRACTOR
        // ==========================

        private string ExtractTaskTitle(string input)
        {
            string title = input;


            string[] removeWords =
            {
        "add task",
        "new task",
        "remind me to",
        "remind me"
    };


            foreach (string word in removeWords)
            {
                title = RemoveTextIgnoreCase(title, word);
            }


            string reminder = ExtractReminder(input);


            if (reminder != null)
            {
                title = RemoveTextIgnoreCase(title, reminder);
            }


            return title.Trim();
        }




        // Compatible with older C# versions
        private string RemoveTextIgnoreCase(string original, string remove)
        {
            int index = original.IndexOf(
                remove,
                StringComparison.OrdinalIgnoreCase);


            while (index >= 0)
            {
                original =
                    original.Remove(index, remove.Length);


                index = original.IndexOf(
                    remove,
                    StringComparison.OrdinalIgnoreCase);
            }


            return original;
        }

        // ==========================
        // REMINDER EXTRACTOR
        // ==========================

        private string ExtractReminder(string input)
        {

            string lower =
                input.ToLower();



            if (lower.Contains("tomorrow"))
                return "tomorrow";



            var match =
            System.Text.RegularExpressions.Regex.Match(
                lower,
                @"in\s+\d+\s+(day|days|week|weeks)");



            if (match.Success)
                return match.Value;



            return null;
        }

        private string GetSentimentPrefix(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("hacked"))
                return $"I understand you're concerned, {userName}. ";
            if (input.Contains("thank") || input.Contains("good") || input.Contains("great"))
                return $"Glad to help, {userName}! ";
            return $"Hey {userName}, ";
        }
    }
}