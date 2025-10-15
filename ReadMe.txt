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
