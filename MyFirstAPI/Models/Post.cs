﻿namespace MyFirstAPI.Models;

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;

        public string Body { get; set; } = String.Empty;

        public int UserId {  get; set; }
    }

