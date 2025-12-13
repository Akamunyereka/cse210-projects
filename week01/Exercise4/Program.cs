using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("Learning C# Basics", "Tech Academy", 420);
        video1.AddComment(new Comment("Alice", "Great explanation, thank you!"));
        video1.AddComment(new Comment("John", "This helped me so much."));
        video1.AddComment(new Comment("Sara", "Clear and simple!"));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("How to Cook Rice", "Kitchen Master", 300);
        video2.AddComment(new Comment("Mike", "Tried it and it worked perfectly."));
        video2.AddComment(new Comment("Anna", "Finally I can cook rice correctly."));
        video2.AddComment(new Comment("David", "Short and helpful."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Exercise at Home", "Fitness Daily", 600);
        video3.AddComment(new Comment("Linda", "I'm sweating already!"));
        video3.AddComment(new Comment("George", "This workout is intense."));
        video3.AddComment(new Comment("Nina", "Thanks for the routine."));
        videos.Add(video3);

        // Display all videos
        foreach (Video video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
