using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ShoutOut.WebApp.Cryptography;

public interface IHashingService
{
    string HashPassword(string password, string salt);
    PasswordWithSalt CreateNewPassword(string password);

    public record PasswordWithSalt(string HashedPassword, string salt);
}

public class HashingService : IHashingService
{
    private static readonly char[] SaltSigns =
    [
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
        'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '!', '@', '#', '$', '$', '%', '^', '1', '2', '3', '4', '5', '6', '7',
        '8', '9', '0'
    ];

    public string HashPassword(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password!,
            saltBytes,
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8));

        return hashed;
    }

    public IHashingService.PasswordWithSalt CreateNewPassword(string password)
    {
        var newSalt = CreateSalt();
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password!,
            newSalt,
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8));

        return new IHashingService.PasswordWithSalt(hashed, Convert.ToBase64String(newSalt));
    }

    private byte[] CreateSalt()
    {
        var salt = new byte[10];
        var random = new Random();
        random.NextBytes(salt);
        return salt;
    }
}