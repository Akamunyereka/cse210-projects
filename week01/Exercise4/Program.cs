using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create 3–4 videos
        Video video1 = new Video("Learning C# Classes", "Nate Dev", 480);
        Video video2 = new Video("How YouTube Recommendations Work", "Tech Explained", 720);
        Video video3 = new Video("Top 10 Productivity Tips", "LifeBoost", 560);

        // Add comments
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("John", "Very helpful, thanks."));
        video1.AddComment(new Comment("Maria", "Clear and easy to understand."));

        video2.AddComment(new Comment("Kevin", "Finally understand the algorithm!"));
        video2.AddComment(new Comment("Sophia", "Well explained."));
        video2.AddComment(new Comment("Daniel", "This helped my project."));

        video3.AddComment(new Comment("Grace", "Tip #4 changed my life!"));
        video3.AddComment(new Comment("Peter", "Awesome content."));
        video3.AddComment(new Comment("Rita", "Taking notes—thanks!"));

        // Put videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display details
        foreach (Video video in videos)
        {
            Console.WriteLine("=====================================");
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLengthSeconds()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($" - {comment.GetCommenterName()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Program finished. Take your screenshot now.");
    }
}
