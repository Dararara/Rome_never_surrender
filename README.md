# Rome_never_surrender
mobile game for CG course project


## 设计文档
### 对话系统
#### TextShow
用于展示单独的一段话，附着在Text上面
核心函数：
Display_text(string str, TextObserver);
在Text中展示str的内容，展示结束调用TextObserver的ShowFinish函数，由TextObserver接口继续处理接下来的工作

#### TalkTree
核心为一颗以数组为底层的有向图，通过hash表实现根据名字查找对应的节点，每个节点为一段对话信息，包括内容，背景，立绘，音乐，名字，子选项等等，根据子选项的名字，通过哈希查找实现O(1)跳转

#### WindowControl
控制整个对话UI，负责接收TalkNode，解析TalkNode然后执行相应的需求
核心函数：
Talk(Node talk_node, Talker talker);
接收talk_node以及对应的talker，解析talk_node，利用TextShow和自身的一些函数展示对话，直到最后，如果有选项，则展示选项，等待点击后返回点击值，如果没有选项，就返回-1，代表什么也没有。

这里，Button的展示和退出做了一些动画效果，但测试过程中出现难以解决的bug，button退出需要一定的时间，如果在inactive之前开始下一个选项分支，会导致刚刚展示出来的button被线程里的hide_buttons隐藏掉，导致button无法显示的恶性bug，解决方案大概是文本稍微长一点，只要撑过一到两秒钟，这样的bug应该就可以避免。

问题解决了，解决方案是我们缩短退出时间，等待button退出之后再展示下一模块，这样可能会慢一点，但更符合逻辑，不会出现两块打架的情况，而且只需要大概0.2秒的时间，可以接受。

#### Talker
暂时不清楚具体该以什么样的形式存在，应该会像麻将一样，自己拥有的是对话树的名字索引，所有对话树统一存储方便管理。

#### 

视角调起来的时候,没有收绿色色块

切出场景时,视角没有复原

文本有一些瑕疵,拜占庭的文本切断点应该后调

