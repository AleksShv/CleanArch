﻿namespace CleanArch.Entities.Base;

public interface IAuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public string LastModifiedBy { get; set; }
    public DateTimeOffset LastModifiedAt { get; set; }
}
