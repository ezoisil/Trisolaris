namespace Trisolaris.Saving
{
    // This interface makes sure that every saveable entity is resbonsible from their own saving.
    // This way saveable entity doesn't have to know too much. It only tells you to capture and restore whatever information you are keeping.
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}