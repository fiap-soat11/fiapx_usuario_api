using System;
using System.Collections.Generic;

namespace Domain;

public partial class Usuario
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }

}
