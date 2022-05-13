namespace Trisolaris.Control
{
    public interface IRaycastable
    {
        bool HandleRaycast(PlayerController callingController);
    }
}