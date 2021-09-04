public enum ClampingType
{
    /// <summary>
    /// Just cuts off a value below 0 or above 1
    /// </summary>
    Clamp,
    /// <summary>
    /// Interpolates values between 0 and 1
    /// </summary>
    Interpolation,
    /// <summary>
    /// No ñlamping, can return values less than 0 or greater than 1
    /// </summary>
    NoClamping
}