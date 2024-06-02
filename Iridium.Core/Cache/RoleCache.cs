namespace Iridium.Core.Cache;

// Static Role Cache
public static class RoleCache
{
    private static readonly Dictionary<string, long> _roleCache = new();
    private static readonly object _cacheLock = new();

    public static void AddOrUpdate(string key, long value)
    {
        lock (_cacheLock)
        {
            _roleCache[key] = value;
        }
    }

    public static bool TryGetValue(string key, out long value)
    {
        lock (_cacheLock)
        {
            return _roleCache.TryGetValue(key, out value);
        }
    }

    public static IEnumerable<KeyValuePair<string, long>> GetAll()
    {
        lock (_cacheLock)
        {
            return new Dictionary<string, long>(_roleCache).AsEnumerable();
        }
    }
}