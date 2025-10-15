public interface IPlayerInput
{
    float Move { get; }

    /// <summary>
    /// Чи тримається кнопка джамп в даний момент.
    /// </summary>
    bool JumpHeld { get; }

    /// <summary>
    /// Час (Time.time) останнього натискання джампу, або -Infinity якщо не було.
    /// </summary>
    float LastJumpPressedTime { get; }

    /// <summary>
    /// Час (Time.time) останнього відпускання джампу, або -Infinity якщо не було.
    /// </summary>
    float LastJumpReleasedTime { get; }
}
