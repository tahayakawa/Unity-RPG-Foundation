# Unity-RPG-Foundation
UnityでRPGを作るのに必要なメニュー画面やデータ管理などの基盤部分の実装
- [ユニティちゃんのRPGを作ってみよう](https://gametukurikata.com/category/letstrymakeit/unitychanrpg/page/3)
- メニュー画面はこちらをもとにして、調整したり、いくつか機能を追加したりしたものです
- Instantiate, GetComponent, Findなどの重い処理をできるだけ使わないようにして軽量化を図っています
- 戦闘画面動画のイラストはAIを利用しています（パッケージには含まれません）

# このパッケージで実装されていること
- キャラクターごとのステータス
- アイテムごとのデータ
- メニュー画面
  - ステータス画面
  - アイテム画面
- ターン制の戦闘画面・システム

https://github.com/tahayakawa/Unity-RPG-Foundation/assets/102804813/51e43906-091a-4a1c-bc6c-8cef8ded0a7f


https://github.com/tahayakawa/Unity-RPG-Foundation/assets/102804813/3f05d472-cc9a-4961-89bb-33d78ebb17c8



# 実装する予定
- アイテム種別ごとのタブ分け
- キャラごとのスキル振り分け機能

# 動作確認
- アセットのインポート
- シーン内にMainCanvas, GameManager, Playerプレハブを置く
  - 参照を適当に渡す
- "Menu"ボタンを割り当てる

![image](https://github.com/tahayakawa/Unity-RPG-Foundation/assets/102804813/d9dc1e6d-6f5f-4812-bce2-9750ed8cff44)

- 実行して動作を確認する

# アイテム・キャラクターデータの生成
- プロジェクトウィンドウの適当な位置で右クリック->Create
- CreateItem: アイテムデータの生成
- CreateAllyStatus: 個別のキャラクター生成
  - 上で作成した装備アイテムを装備できる
- CreatePartyStatus: パーティの情報
  - 上で生成したアイテムやキャラクターを割り当てる
  - ここで割り当てられているアイテムやキャラクターがメニュー画面で表示される持ち物やステータスとなる

# 使用させていただいたエディタ拡張など
- [インターフェイスをSerialize出来るようにするSerializeReferenceのための表示attribute](https://qiita.com/tsukimi_neko/items/7922b2433ed4d8616cce
)https://qiita.com/tsukimi_neko/items/7922b2433ed4d8616cce

以下は別途インストールが必要
- [SerializableDictionary](https://assetstore.unity.com/packages/tools/integration/serializabledictionary-90477)https://assetstore.unity.com/packages/tools/integration/serializabledictionary-90477
- [UniTask ver.2.5.0](https://github.com/Cysharp/UniTask/releases)https://github.com/Cysharp/UniTask/releases
