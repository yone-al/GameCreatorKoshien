# CLAUDE.md

このファイルは、このリポジトリでコード作業を行う際のClaude Code (claude.ai/code) への指針を提供します。

## プロジェクト概要

「GameCreatorKoshien」は、上から落ちてくる障害物を避けるUnity 2Dゲームプロジェクトです。Unity 6000.0.50f1とUniversal Render Pipeline (URP)を使用しています。

## アーキテクチャ

### コアゲームコンポーネント
- **PlayerController.cs**: プレイヤーの体力（HPシステム）と障害物との衝突検知を管理
- **ObstacleController.cs**: 障害物の動作制御（下向きに-5の速度で落下）
- **Input System**: Unity の新しいInput Systemパッケージを使用した入力処理
- **2D Physics**: 障害物との相互作用にトリガー衝突を使用するUnityの2D物理システム

### Unityプロジェクト構造
- **Assets/Scripts/**: すべてのC#ゲームスクリプト
- **Assets/Scenes/**: Unityシーンファイル（GameScene.unity、SampleScene.unity）
- **Assets/Settings/**: URPレンダリング設定とシーンテンプレート
- **ProjectSettings/**: Unityプロジェクト設定ファイル
- **Packages/**: Package Managerで管理されるUnityパッケージ依存関係

## 開発コマンド

### プロジェクトのビルド
- Unity Editorでプロジェクトを開く
- UnityのBuild Settings（File → Build Settings）を使用してターゲットプラットフォーム向けにビルド
- 外部IDE開発用のソリューションファイルは `GameCreatorKoshien.sln`

### テスト
- Unity Test Frameworkがユニットテストと統合テスト用にインストール済み
- Unity Editorからテスト実行: Window → General → Test Runner

### 依存関係
プロジェクトでは以下の主要なUnityパッケージを使用：
- Universal Render Pipeline (URP) 17.0.4
- Input System 1.14.0
- 2D Feature package 2.0.1
- Cursor IDE integration（カスタムパッケージ）
- Timeline、Visual Scripting、UIパッケージ

## 開発時の注意事項

### 物理システム
- 障害物の物理演算に2D Rigidbodyコンポーネントを使用
- プレイヤーと障害物間でトリガー衝突検知を実行
- 障害物は識別用に「Obstacle」タグでタグ付け

### ゲームロジック
- プレイヤーは3HPでスタート
- 障害物との衝突時にHPが1減少
- ゲーム状態はコンソールログで追跡

### IDE統合
- Visual StudioとJetBrains Riderの両方をサポート
- 開発体験向上のためのカスタムCursor IDEパッケージをインストール済み