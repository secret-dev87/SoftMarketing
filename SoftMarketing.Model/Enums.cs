using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    
    public enum TemplateNames
    {
        Adwa_Victory_Day = 12,
        Birthday = 1,
        Christmas = 7,
        Custom = 2,
        Eid = 9,
        Event = 8,
        Feedback = 3,
        Happy_New_Year = 13,
        Independence_Day = 10,
        Promotion = 4,
        Reference = 5,
        Reminder = 6,
    }
    public enum SettingsNames
    {
        default_reminder_duration = 5,
        delete_customer_after_reminders = 1,
        disable_reminders = 4,
        user_language = 6,
        user_local_time = 2,
        user_preferred_message_sending_time = 3,
    }
    public enum MessagingApp
    {
        Telegram = 4,
        Viber = 5,
        WhatsApp = 6,
    }

    public enum SocialApp
    {
        Facebook = 1,
        Instagram = 2,
        Snapchat = 3,
    }
    public enum ReminderDuration
    {
        One = 1,
        Three = 3,
        Six = 6,
        Twelve = 12
    }
    public enum ReminderTimes
    {
        One = 1,
        Two = 1,
        Three = 3,
        Four = 3,
        Five = 3,
        Six = 6,
    }

    public enum HasTemplate
    {
        No = 1,
        Yes = 2,
    }
    public enum Blocked
    {
        No = 1,
        Yes = 2,
    }
    public enum VisitPurpose
    {
        Sales = 1,
        Renewal = 2,
        New_device_installation = 3,
        Troubleshoot = 4,
    }
    public enum Hiring
    {
        online = 1,
        insurance = 2,
    }
    public enum TemplateTypes
    {
        Reminder = 1,
        Promotion = 2,
        Birthday = 3,
        Events = 4,
        Feedback = 5,
    }
    public enum YesNo
    {
        No = 0,
        Yes = 1,
    }

    public enum UserStatus
    {
        inactive = 1,
        active = 2,
        blocked = 3,
    }

    public enum ToastLevel
    {
        Info,
        Success,
        Warning,
        Error
    }

}
