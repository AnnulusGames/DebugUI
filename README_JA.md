# DebugUI
 A framework for building debugging tools built on Unity UI Toolkit.

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE)
![unity-version](https://img.shields.io/badge/unity-2022.1+-000.svg)
[![releases](https://img.shields.io/github/release/AnnulusGames/DebugUI.svg)](https://github.com/AnnulusGames/DebugUI/releases)

[English README is here.](README.md)

DebugUIはUnity UI Toolkit上に構築された、デバッグ用のGUIツールを作成するためのフレームワークです。専用のBuilderを使用することで、簡単かつ迅速にランタイムで動作するデバッグツールの作成が可能になります。

```cs
public class DemoUIBuilder : DebugUIBuilderBase
{
    ...

    protected override void Configure(IDebugUIBuilder builder)
    {
        builder.ConfigureWindowOptions(options =>
        {
            options.Title = "Demo";
            options.Draggable = true;
        });

        builder.AddFoldout("Physics", builder =>
        {
            builder.AddSlider("Time Scale", 0f, 3f, () => Time.timeScale, x => Time.timeScale = x);
            builder.AddSlider("Gravity Scale", 0f, 3f, () => GravityScale, x => GravityScale = x);
            builder.AddButton("Add Circle", () => Instantiate(prefab));
            builder.AddButton("Reload Scene", () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        });

        builder.AddFoldout("Post-processing", builder =>
        {
            builder.AddSlider("Hue Shift", -180f, 180f, () => colorAdjustments.hueShift.value, x => colorAdjustments.hueShift.value = x);
            builder.AddSlider("Bloom Intensity", 0f, 10f, () => bloom.intensity.value, x => bloom.intensity.value = x);
        });
    }
}
```

![img](docs/images/img1.png)

## セットアップ

### 要件

* Unity 2022.1 以上

### インストール

1. Window > Package ManagerからPackage Managerを開く
2. 「+」ボタン > Add package from git URL
3. 以下のURLを入力する

```
https://github.com/AnnulusGames/DebugUI.git?path=src/DebugUI/Assets/DebugUI
```

またはPackages/manifest.jsonを開き、dependenciesブロックに以下を追記

```json
{
    "dependencies": {
        "com.annulusgames.debug-ui": "https://github.com/AnnulusGames/DebugUI.git?path=src/DebugUI/Assets/DebugUI"
    }
}
```

## デバッグウィンドウを作成する

DebugUIを用いてデバッグ用のウィンドウを作成するには、以下の手順に従います。

### 1A. DebugUIBuilderBaseクラスを継承したコンポーネントを作成する

`DebugUIBuilderBase`クラスを継承し、`Configure()`メソッドを実装することで、簡単にデバッグウィンドウを構成することができます。

```cs
using UnityEngine;
using DebugUI; 

public class DebugUIBuilerExample : DebugUIBuilderBase
{
    [SerializeField] float field;

    protected override void Configure(IDebugUIBuilder builder)
    {
        builder.AddLabel("Label");
        builder.AddButton("Button", () => Debug.Log("Hello!"));
        builder.AddField("Field", () => field, x => field = x);
    }
}
```

<img src="https://github.com/AnnulusGames/DebugUI/blob/main/docs/images/img2.png" width="500">

このコンポーネントを適当なGameObjectにアタッチし、Inspectorで対象のUIDocumentを指定します。

`DebugUIBuilderBase`は`MonoBehaviour`を継承したクラスで、Awake時に設定されたUIDocument上にデバッグウィンドウを作成します。

### 1B. DebugUIBuilderクラスを使用する

新たなコンポーネントの追加を避けたい場合には、`DebugUIBuilder`クラスを用いてデバッグウィンドウの作成を行うことも可能です。

```cs
UIDocument uiDocument;

var builder = new DebugUIBuilder();
builder.AddLabel("Label");
builder.AddButton("Button", () => Debug.Log("Hello!"));
builder.AddField("Field", () => field, x => field = x);
builder.BuildWith(uiDocument);
```

構成したデバッグウィンドウをビルドするには`BuildWith()`メソッドを呼び出します。

### 2. Theme Style Sheetを適用する

DebugUIでは、モダンなGUIのスタイルを提供するStyle Sheet (uss)およびTheme Style Sheet (tss)が用意されています。(ファイルは`Packages/com.annulusgames.debug-ui/Package Resources`フォルダ内に置かれています。)

使用するテーマは、UI Toolkitを導入するとAssetsフォルダ内に生成されるPanel Settingsアセットから変更できます。

<img src="https://github.com/AnnulusGames/DebugUI/blob/main/docs/images/img3.png" width="500">

既存のテーマと併用したい場合には、使用するtssアセットのStyle SheetsにDebugUIのussを追加してください。

## 利用可能な要素

### Label

<img src="https://github.com/AnnulusGames/DebugUI/blob/main/docs/images/example-label.png" width="400">

```cs
builder.AddLabel("Label");
```

### Button

<img src="https://github.com/AnnulusGames/DebugUI/blob/main/docs/images/example-button.png" width="400">

```cs
builder.AddButton("Button", () => Debug.Log("Hello!"));
```

### Field

<img src="https://github.com/AnnulusGames/DebugUI/blob/main/docs/images/example-field.png" width="400">

```cs
float floatValue;

builder.AddField("Field", () => floatValue, x => floatValue = x);
builder.AddField("Read-Only Field", () => floatValue);
```

> [!NOTE]
> `AddField()`は現在、`bool`, `int`, `float`, `string`, `Enum`, `Vector2`, `Vector3`, `Vector4`, `Vector2Int`, `Vector3Int`, `Rect`, `RectInt`, `Bounds`, `BoundsInt`に対応しています。

> [!TIP]
> `AddField()`で作成したフィールドは対象の値と双方向にバインドされ、フィールドまたは元の値に変更を加えると自動的にもう片方へ反映されます。

### Slider

<img src="https://github.com/AnnulusGames/DebugUI/blob/main/docs/images/example-slider.png" width="400">

```cs
float floatValue;
int intValue;

builder.AddSlider("Slider", 0f, 1f, () => floatValue, x => floatValue = x);
builder.AddSlider("Slider Int", 0, 100, () => intValue, x => intValue = x);
```

### Progress Bar

<img src="https://github.com/AnnulusGames/DebugUI/blob/main/docs/images/example-progress.png" width="400">

```cs
float floatValue;

builder.AddProgressBar("Progress", 0f, 1f, () => floatValue);
```

### Image

<img src="https://github.com/AnnulusGames/DebugUI/blob/main/docs/images/example-image.png" width="400">

```cs
Texture2D texture2D;
Sprite sprite;
RenderTexture renderTexture;
SpriteRenderer spriteRenderer;

builder.AddImage("Texture2D", texture2D);
builder.AddImage("Sprite", sprite);
builder.AddImage("Render Texture", renderTexture);
builder.AddImage("Dynamic", () => spriteRenderer.sprite);
```

### Foldout

<img src="https://github.com/AnnulusGames/DebugUI/blob/main/docs/images/example-foldout.png" width="400">

```cs
float floatValue;

builder.AddFoldout("Foldout", builder =>
{
    builder.AddField("Field", () => floatValue, x => floatValue = x);
    builder.AddButton("Button", () => Debug.Log("Hello!"));
});
```

## ウィンドウの設定

ウィンドウの表示等の設定は`ConfigureWindowOptions()`から行うことができます。

```cs
builder.ConfigureWindowOptions(options =>
{
    options.Title = "Custom Title";
    options.Draggable = false;
});
```

| プロパティ | 説明 |
| - | - |
| Draggable | ウィンドウをドラッグ可能か (デフォルト値はtrue) |
| Title | ウィンドウのタイトル |

## ライセンス

[MIT License](LICENSE)