## Update README.md - Under Construction

#### This Readme is a work in progress. Once it's completed, an English version will be provided.
---
<details>
  <summary>Language: English</summary>
  <ul>
    <li><a href="README.md">English</a></li>
    <li><a href="README.ja.md">Japanese</a></li>
  </ul>
</details>

![Version: 0.8.0](https://img.shields.io/badge/version-0.8.0-pink.svg)
![License: AGPL-1.0-only](https://img.shields.io/badge/License-AGPL%201.0%20only-red.svg)
![Platform: .NET 8](https://img.shields.io/badge/platform-.NET%208-green)
[![Documentation: online](https://img.shields.io/badge/docs-online-purple.svg)](https://shimodateakira.github.io/VirtualStorageLibrary/)
![Maintenance: active](https://img.shields.io/badge/maintenance-active-blue.svg)

# VirtualStorageLibraryへようこそ！

## プロジェクトの概要と目的
VirtualStorageLibraryは、完全にオンメモリで動作し、ツリー構造コレクションを提供する.NETライブラリです。
このライブラリは、**データの階層的な構造を管理するための基盤**を提供し、 ユーザー定義型<T>を内包するアイテム、ディレクトリ、シンボリックリンクをサポートします。
このライブラリは **ファイルシステムではありません。** 
従来のファイルシステムの概念を参考にしつつ、より柔軟で使いやすいツリー構造を実現するために **ゼロから再設計** しました。
このライブラリは、ユーザーが **パスの指定による** ノードの参照、探索、操作を **直感的** に行えるようにすることを目的としています。

## 目次

- [主な機能](#主な機能)
- [想定されるユースケース](#想定されるユースケース)
- [技術スタック](#技術スタック)
- [ターゲットユーザー](#ターゲットユーザー)
- [インストール方法](#インストール方法)

[[↑](#目次)]
## 主な機能

#### 柔軟なツリー構造
  親子関係に基づく階層的なデータ構造を提供し、柔軟なノード管理が可能です。

#### 多様なノードのサポート
  ユーザー定義型\<T\>を含むアイテム、ディレクトリ、シンボリックリンクをサポートします。

#### パスによる直感的なノード操作
  パスを指定することでノードの参照、探索、追加、削除、変名、コピーおよび、移動が容易に行え、使いやすいAPIを提供します。

#### リンク管理
  リンク辞書を使ったシンボリックリンクの変更を管理し、ターゲットパスの変更を追跡します。

#### 循環参照防止
  シンボリックリンクを含んだパスを解決時、ディレクトリを循環参照するような構造を検出した場合、例外をスローします。

#### 柔軟なノードリストの取得
  ディレクトリ内のノードのリストを取得する際、指定されたノードタイプでフィルタ、グルーピングし、指定された属性でソートした結果を取得します。

[[↑](#目次)]
## 想定されるユースケース

### 自然言語処理(NLP)
自然言語処理の分野では、テキストデータの解析や文書の構文解析に**ツリー構造**が頻繁に利用されます。
例えば、文章の構文解析結果を**構文木**として表現し、文の各要素の関係性を視覚化します。
このような**構造化データの管理**にはツリー構造が非常に有効です。

仮想ストレージライブラリは、こうした**ツリー構造データの管理**や**パスを使ったノードアクセス**をサポートし、
**効率的なデータ解析**を可能にします。具体的には、以下のようなシナリオで役立ちます：

- **構文木の管理**: 文法解析結果の構文木をノードとして保存し、ノード間の関係を階層的に管理します。
- **エンティティリンクの管理**: テキスト内のエンティティ（人名、地名など）の関係をツリー構造で表現し、迅速な検索とアクセスをサポートします。
- **トピックモデルの可視化**: トピック間の階層的な関係をモデル化し、複数のトピックやサブトピックを効率的に表示します。

これにより、NLPタスクにおいて**複雑なデータ構造の管理**が容易になり、**解析効率が向上**します。

### ナレッジベースシステム
ナレッジベースシステムでは、大量の文書や情報を**階層的に整理**し、**効率的な検索性**を提供することが求められます。
仮想ストレージライブラリは、こうした情報の**階層構造を管理**し、ユーザーが**必要な情報に迅速にアクセス**できるようにします。

具体的なシナリオとしては、以下のようなものがあります：
- **技術文書の管理**: 製品の技術文書やマニュアルをカテゴリごとに分類し、ユーザーが特定の情報を素早く見つけられるようにします。
- **FAQシステム**: よくある質問とその回答を階層的に整理し、検索性を高め、ユーザーが簡単に回答を見つけられるようにします。
- **知識ベースの構築**: 組織内の専門知識を文書化し、ツリー構造で管理することで、新しいメンバーが素早く学習できる環境を提供します。

これにより、ナレッジベースシステムは**情報の整理とアクセスの効率化**を実現し、**情報の利活用を最大化**します。

### ゲーム開発
ゲーム開発において、ゲーム内の**オブジェクトやシーンの管理**は重要です。
特に、オブジェクトの**階層関係の管理**が必要な場合、仮想ストレージライブラリは、シーンの**ダイナミックな変更**をサポートし、
**開発プロセスの効率化**に寄与します。

具体的なシナリオとしては、以下のようなものがあります：
- **シーン管理**: ゲーム内の異なるレベルやエリアのオブジェクトを階層的に管理し、プレイヤーの行動やイベントに応じて動的に変更します。
- **キャラクターの装備管理**: キャラクターが装備するアイテムや武器をツリー構造で管理し、リアルタイムでの装備変更を容易に行います。
- **レベルデザインの効率化**: レベルデザイナーがシーンやオブジェクトの階層を視覚的に管理し、配置や変更を迅速に行えるようにします。

これにより、ゲーム開発プロセスは**柔軟性とスピード**を持ち、**クリエイティブな要素**を最大限に活かすことができます。

### 階層型クラスタリング
データの**階層的なグループ化や分類**を行う階層型クラスタリングでは、クラスタリング結果を**ツリー構造で管理**し、
**データの分析と可視化**をサポートします。仮想ストレージライブラリは、このような分析プロセスを支援します。

具体的なシナリオとしては、以下のようなものがあります：
- **顧客セグメンテーション**: 顧客データを購買行動やデモグラフィック情報に基づいて階層的に分類し、マーケティング戦略の最適化を図ります。
- **生物分類学**: 種や属、科などの生物の分類情報をツリー構造で管理し、各レベルの分類における特性や関係を可視化します。
- **階層型データ分析**: 大規模データセットを階層的にグループ化し、データのパターンやトレンドを発見するための探索的データ解析を行います。

これにより、階層型クラスタリングの結果を**効率的に管理**し、**視覚的な洞察**を得ることが可能になります。仮想ストレージライブラリは、
このような**データの可視化と分析のプロセス**を支援し、データドリブンな意思決定をサポートします。

### 教育と学習
仮想ストレージライブラリのソースコードは、教育と学習の分野で活用できます。
特に、プログラミング教育やデータ構造の理解を深めるために効果的です。

具体的なシナリオとしては、以下のようなものがあります：

- **プログラミング教育**:
  学生がツリー構造データの管理や操作方法を学ぶための実習教材として使用できます。
  具体的な課題を通じて、仮想のディレクトリ構造やデータツリーを操作することで、データ構造やアルゴリズムの理解を深めます。

- **データ構造の可視化**:
  データ構造を視覚的に表現することで、学生はデータの階層構造や関係性を直感的に理解できます。
  これにより、複雑なデータ構造の概念をより容易に学ぶことが可能です。

- **再帰プログラミング、コレクションの操作、遅延評価の学習**:
  学生は仮想ストレージライブラリを使用して、再帰的なアルゴリズムの設計、コレクションデータの操作、遅延評価のテクニックなど、
  重要なプログラミングの概念を実践的に学ぶことができます。
  これにより、プログラミングの基礎から応用までのスキルを身につけることができます。

これにより、仮想ストレージライブラリは教育現場において、学生や学習者が実践的なプログラミングスキルやデータ構造の理解を深める手助けをします。

[[↑](#目次)]
## 技術スタック

### 概要
VirtualStorageLibraryは、.NET 8プラットフォーム上で開発され、C#言語を使用しています。  

### プログラミング言語
- **C#**: プロジェクトの主要な開発言語です。C#のバージョンは12を使用しています。

### フレームワークとライブラリ
- **.NET 8**: プロジェクトの基盤となるフレームワークで、高性能なアプリケーションを構築します。  
              また、.NET 8をサポートする複数のプラットフォームで動作する事が可能です。

### ツールとサービス
- **Visual Studio 2022**: プロジェクトの主要な開発環境です。  
  詳細は、[Visual Studioの公式サイト](https://visualstudio.microsoft.com/vs/)で確認できます。
- **GitHub**: 開発リソースを管理するプラットフォームです。  
  詳細は、[GitHub ドキュメント](https://docs.github.com/)で確認できます。
- **DocFX**: ドキュメントを生成する高機能なツールです。  
  詳細は、[DocFXの公式サイト](https://dotnet.github.io/docfx/)で確認できます。
- **DocFX Material**: DocFX用のスタイルシートとテンプレートを提供し、ドキュメントの見栄えを向上させるために使用しています。  
  詳細は、[DocFX Materialの公式サイト](https://m3.material.io/)で確認できます。

[[↑](#目次)]
## ターゲットユーザー

### 主なユーザー層
仮想ストレージライブラリは、幅広いユーザー層を対象としています。以下は、主なユーザー層の例です：

- **開発者**: .NETやC#を使用しているソフトウェア開発者。特にツリー構造データの管理に関心がある方が対象です。このライブラリは、複雑なデータ構造を簡単に扱うためのツールとして利用できます。
- **データサイエンティスト**: 複雑なデータ構造の解析やモデリングに従事する専門家。特に、ツリー構造データの効率的な管理や分析が求められる状況で役立ちます。
- **教育関係者と学生**: プログラミング教育やデータ構造の学習に興味がある方。実践的なプログラミングスキルやデータ構造の理解を深めるための教材として使用できます。

### 使用目的
仮想ストレージライブラリは、以下のような目的で使用されます：

- **データの管理と解析**: ツリー構造データを効率的に管理し、解析するためのツールとして。データの階層的な組織化や検索が可能です。
- **データの組織化と整理**: 大量のデータを階層的に整理し、視覚的に理解しやすい構造にするため。データの関係性を明確にするのに役立ちます。
- **教育と学習**: プログラミングやデータ構造の基本概念を学ぶ教材として。再帰プログラミング、コレクションの操作、遅延評価などの概念を学ぶことができます。

### 対象とする業界
仮想ストレージライブラリは、特に以下の業界での利用を想定しています：

- **IT**: ソフトウェア開発やデータサイエンス分野での利用。効率的なデータ管理と解析が求められるプロジェクトで役立ちます。
- **教育**: プログラミング教育や学習の分野。教育関係者や学生が実践的なスキルを習得するためのツールとして使用できます。

## 開発状況と今後の予定
現在 (2024/08/09) 、V1.0.0で実装すべき必要な機能は全て実装済みです。  
しかし、数件のバグ修正と、30件近い機能改善、リファクタリングが残っている状況です。  
V0.8.0では、この状態でユーザーの皆様に試用して頂き、フィードバックを頂きたいと考えています。  
フィードバックには、バグ報告や機能改善の提案などが含まれます。  
それと同時に、V0.9.0に向けて残作業を消化していく予定です。  
V0.9.0のリリースは2024年10月を予定しています。  
なお、この期間中、ライブラリで提供している機能のクラス名、メソッド名、プロパティ名等は予告なく変更、統合、廃止する事があります。
その場合、リリースノートに詳細を掲載するのでご確認ください。
詳細は、[現在の問題点と改善案](https://github.com/users/shimodateakira/projects/3/views/3)を参照してください (日本語)。

[[↑](#目次)]
## インストール方法

### Visual Studio 2022 でのインストール
#### **NuGet パッケージマネージャーからインストール**する方法:
   - Visual Studio 2022 のソリューションエクスプローラーで、プロジェクトを右クリックし、「NuGet パッケージの管理」を選択します。
   - 「参照」タブで `VirtualStorageLibrary` を検索し、選択してインストールします。
#### **パッケージマネージャーコンソールからインストール**する方法:
   - Visual Studio 2022 のメニューから「ツール」>「NuGet パッケージマネージャー」>「パッケージマネージャーコンソール」を選択します。
   - パッケージマネージャーコンソールで以下のコマンドを入力し、インストールします。
```
Install-Package VirtualStorageLibrary -Version 0.8.0
```

### .NET CLIを使用したインストール
- コマンドラインで、プロジェクトファイル (`.csproj`) があるディレクトリに移動します。
- Visual Studio 2022が起動していないことを確認してください。
- 以下のコマンドを入力して、`VirtualStorageLibrary` をインストールします。  
この方法でプロジェクトファイルに自動的にパッケージが追加されます。
```
dotnet add package VirtualStorageLibrary --version 0.8.0
```

### インストールの確認
インストールが成功すると、プロジェクトの依存関係に `VirtualStorageLibrary` が追加され、使用できるようになります。  
インストール後、必要に応じて `using` ディレクティブを追加してライブラリを参照してください。





