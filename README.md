# NewRepo
# Cybersecurity Chatbot - Task Management System

## Project Overview

The Cybersecurity Chatbot is a task management application designed to help users organize cybersecurity-related activities, manage reminders, and track task completion. The system integrates a MySQL database to store and retrieve user tasks efficiently.

---

# Features

## 1. User Task Management

### Add New Tasks
- Users can create new tasks.
- Each task stores:
  - Username
  - Task name
  - Task description
  - Reminder date
  - Completion status
  - Creation date

Example:
- Task: "Update Antivirus"
- Description: "Install latest security updates"
- Reminder: 30 June 2026

---

## 2. Database Integration

- Connected to a MySQL database.
- Uses secure parameterized SQL queries.
- Automatically stores and retrieves task information.
- Prevents SQL injection attacks through prepared statements.

Database:
