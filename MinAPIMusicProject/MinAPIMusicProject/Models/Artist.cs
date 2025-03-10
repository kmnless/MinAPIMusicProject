﻿namespace MinAPIMusicProject.Models;

public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Track> Tracks { get; set; }
}