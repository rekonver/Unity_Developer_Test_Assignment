Below in Ukrainian

Version: Unity 6000.0.32f1, Input System and TextMeshPro packages installed

### 1. PlayerInput.cs

Implements `IPlayerInput` using the New Input System.
Reads horizontal movement (`Move`) and jump button (`Jump`).
Tracks the last jump press/release times (`LastJumpPressedTime`, `LastJumpReleasedTime`).
Connects `InputActionAsset` and respective actions via `OnEnable`/`OnDisable`.

### 2. PlayerController.cs

Main player controller script.
Supports horizontal movement (`moveSpeed`, `acceleration`, `linearDrag`), jump buffering (`jumpBufferTime`) and coyote time (`coyoteTime`), multiple air jumps (`maxAirJumps`), improved falling (`fallGravityMultiplier`) and short jumps (`jumpCutGravityMultiplier`).
Uses `Rigidbody2D` and ground check via `OverlapCircle`.
Movement, jump, and gravity logic are separated into individual methods.

### 3. PlayerJumpColorCharge.cs

Changes player color while holding the jump button.
Uses `startColor` and `endColor` for smooth transition.
Hold time considers `coyoteTime` from `PlayerController`.
Color smoothly returns to start color when the button is released.

### 4. SceneLoader.cs

Loads scenes by name via `LoadSceneByName(string)`.
`QuitGame()` method for exiting the game: stops Play Mode in the editor, quits the application in build.

### 5. MainMenuFade.cs

Provides a simple fade-in animation for `CanvasGroup`.
Gradually increases `canvasGroup.alpha` from 0 to 1.

### 6. SpriteAnimator.cs

Simple sprite animation script using `SpriteRenderer`.
`frames` array holds animation frames.
Frames are switched at a rate of `framesPerSecond`.

### 7. ICollectible.cs

Interface for collectible items.
Defines `Collect(GameObject collector)` method for pickup behavior.

### 8. Coin.cs

Implementation of `ICollectible` for coins.
Coin rotates (`rotationSpeed`), detects collection via `OnTriggerEnter2D` and player layer (`playerLayer`), invokes `OnCollected` event on pickup, optional collection sound (`collectSound`).
Coin is destroyed after being collected.

### 9. PlayerCollectibleCounter.cs

Tracks the number of collected coins (`CoinCount`).
Invokes `OnCoinCountChanged` event on each collection.
Outputs a Debug log for verification.

### 10. CoinCounterUI.cs

Displays collected coin count in UI (`TextMeshProUGUI`).
Subscribes to `PlayerCollectibleCounter.OnCoinCountChanged`.
Automatically updates text when coin count changes.

### Conclusion

This set of scripts provides player movement and jumping with coyote time and jump buffering, smooth color transition while holding jump, coin collection and UI updates, sprite animation and simple menu fade-in, as well as scene loading and game exit functionality.


Версія Unity 6000.0.32f1 підключений пакет Input System та TMP

### 1. PlayerInput.cs

Реалізує `IPlayerInput` через New Input System.
Читає горизонтальний рух (`Move`) і кнопку стрибка (`Jump`).
Відстежує час останнього натискання/відпускання кнопки стрибка (`LastJumpPressedTime`, `LastJumpReleasedTime`).
Підключає `InputActionAsset` і відповідні дії через `OnEnable`/`OnDisable`.

### 2. PlayerController.cs

Основний керуючий скрипт гравцем.
Підтримує горизонтальний рух (`moveSpeed`, `acceleration`, `linearDrag`), стрибки з буферизацією (`jumpBufferTime`) і coyote time (`coyoteTime`), множинні стрибки в повітрі (`maxAirJumps`), поліпшене падіння (`fallGravityMultiplier`) та короткі стрибки (`jumpCutGravityMultiplier`).
Використовує `Rigidbody2D` та перевірку на землю через `OverlapCircle`.
Логіку руху, стрибка і гравітації розділено на окремі методи.

### 3. PlayerJumpColorCharge.cs

Змінює колір гравця під час утримання стрибка.
Використовує `startColor` і `endColor` для плавного переходу.
Час утримання кнопки враховує `coyoteTime` з `PlayerController`.
При відпусканні кнопки колір плавно повертається до початкового.

### 4. SceneLoader.cs

Завантаження сцен по імені через `LoadSceneByName(string)`.
Метод `QuitGame()` для виходу з гри: в редакторі Unity зупиняє Play Mode, у збірці закриває додаток.

### 5. MainMenuFade.cs

Додає просту fade-in анімацію для CanvasGroup.
Поступово збільшує `canvasGroup.alpha` від 0 до 1.

### 6. SpriteAnimator.cs

Простий скрипт для анімації спрайтів через `SpriteRenderer`.
Масив `frames` — кадри анімації.
Зміна кадрів відбувається з частотою `framesPerSecond`.

### 7. ICollectible.cs

Інтерфейс для колекційних предметів.
Метод `Collect(GameObject collector)` для підбору.

### 8. Coin.cs

Реалізація `ICollectible` для монет.
Монета крутиться (`rotationSpeed`), визначає підбір через `OnTriggerEnter2D` і шар гравця (`playerLayer`), викликає подію `OnCollected` при зборі, можна додати звук збору (`collectSound`).
Після збору монета видаляється з сцени.

### 9. PlayerCollectibleCounter.cs

Відстежує кількість зібраних монет (`CoinCount`).
Викликає подію `OnCoinCountChanged` при кожному зборі.
Виводить Debug лог для перевірки роботи.

### 10. CoinCounterUI.cs

Відображає кількість зібраних монет у UI (`TextMeshProUGUI`).
Підписується на подію `PlayerCollectibleCounter.OnCoinCountChanged`.
Автоматично оновлює текст при зміні кількості монет.

### Висновок

Цей набір скриптів забезпечує рух гравця і стрибки з “coyote time” і буфером стрибка, плавну зміну кольору при утриманні стрибка, збір монет і оновлення UI, анімацію спрайтів і простий fade-in меню, а також завантаження сцен і вихід з гри.
