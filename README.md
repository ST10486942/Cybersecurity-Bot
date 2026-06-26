The Cybersecurity Awareness Bot is a desktop chatbot application designed to educate users about cybersecurity, provide security advice, manage tasks/reminders, track user activity, and test cybersecurity knowledge through an interactive quiz.

---

# 🚀 Features

## 1. 🤖 Intelligent Cybersecurity Chatbot
- Provides cybersecurity-related conversations and guidance.
- Responds to user questions using keyword-based AI responses.
- Supports multiple cybersecurity topics including:
  - Password security
  - Phishing
  - Scams
  - Privacy protection
  - Malware
  - Ransomware
  - VPNs
  - Firewalls
  - Antivirus
  - Encryption
  - Backup strategies
  - Social engineering
  - Identity theft
  - Safe browsing
  - Two-factor authentication (2FA)

---

# 2. 👤 User Personalization
- Allows users to enter their name when launching the application.
- Personalizes chatbot responses using the user's name.
- Provides friendly greetings and sentiment-based responses.

Examples:
- "Hello Tetelo!"
- "I understand you're concerned, Tetelo."

---

# 3. 🗣️ Natural Language Command Recognition
The chatbot understands different ways of requesting features.

Examples:


add task Enable 2FA

remind me to update antivirus tomorrow

show my tasks

start quiz


---

# 4. ✅ Task Management System

Users can create and manage cybersecurity-related tasks.

Features:
- Add new tasks
- Store tasks in a MySQL database
- View saved tasks
- Mark tasks as completed
- Track pending and completed tasks

Examples:


Add task Change password


Creates:


Task:
Change password

Status:
Pending


---

# 5. ⏰ Reminder System

The chatbot supports natural language reminders.

Supported reminders:


tomorrow

in 3 days

in 2 weeks


Example:


Remind me to enable firewall tomorrow


The system automatically calculates and stores the reminder date.

---

# 6. 🗄️ MySQL Database Integration

The application connects to a MySQL database for permanent task storage.

Database features:
- Save user tasks
- Store descriptions
- Store reminder dates
- Store completion status
- Retrieve tasks when requested

---

# 7. 📜 Activity Logging System

Tracks user interactions and system events.

Logs include:

- User messages
- Tasks added
- Tasks completed
- Quiz activity
- Topics discussed
- System startup events

Example:


[14:30:10] User Input - phishing

[14:31:02] Task Added - Enable 2FA

[14:32:15] Quiz Started


---

# 8. 📋 Activity Log GUI Viewer

The application includes a graphical activity log viewer.

Features:
- View recent activities
- Display timestamps
- Display actions performed
- Separate activity log window

---

# 9. 🧠 Cybersecurity Mini Game / Quiz

Interactive cybersecurity quiz system.

Features:
- Multiple-choice questions
- Score tracking
- Instant feedback
- Explanations after answers
- Percentage score calculation

Example:


Question:
What does 2FA stand for?

A) Two Factor Authentication
B) Two Firewall Access

Answer:
A


---

# 10. 🏆 Quiz Scoring System

The chatbot calculates:

- Correct answers
- Total score
- Percentage

Performance feedback:


90%+
Cybersecurity expert

70%+
Great job

50%+
Good effort

Below 50%
Keep practicing


---

# 11. 🎨 Graphical User Interface (WPF)

The application includes a modern desktop interface.

GUI features:
- Chat window
- User input box
- Send button
- Status messages
- Activity log button
- Cybersecurity-themed design

---

# 12. 🎭 ASCII Welcome Animation

The application displays:

- Startup logo
- Welcome ASCII art
- Personalized greeting

Example:


WELCOME Tetelo


---

# 13. 🔊 Audio Support

Includes an audio player component for chatbot interactions and startup experience.

---

# 14. 🛡️ Cybersecurity Education

The bot teaches users about:

- Protecting accounts
- Avoiding online scams
- Securing personal information
- Safe internet habits
- Threat prevention

---

# 15. ⚡ Error Handling

The application includes exception handling for:

- Database connection errors
- Task failures
- Runtime issues
- User input problems

---

# 16. 🔌 Database Connection Testing

The application can test whether the MySQL database connection is working.

Features:
- Connection verification
- Error reporting
- Debug messages

---

# 17. 📱 User-Friendly Interaction

The chatbot supports:

- Keyboard Enter key sending
- Chat-style conversation
- Friendly responses
- Clear feedback messages

---

# 18. 🏗️ Object-Oriented Design

The application uses separate classes:

MainWindow
|
ChatbotEngine
|

| | |
TaskManager MiniGameManager ActivityLogger


Benefits:
- Easier maintenance
- Better organization
- Reusable components
- Cleaner code structure


---

# 🛠️ Technologies Used

- C#
- WPF (.NET Desktop Application)
- MySQL Database
- MySQL Connector
- Object-Oriented Programming (OOP)


---

# Future Improvements

Possible future additions:

- Voice recognition
- AI-powered responses
- User accounts
- Email reminders
- Task priorities
- Dark/light mode
- More cybersecurity quizzes
