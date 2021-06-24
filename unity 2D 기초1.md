### Unity 2D 기초 

* 2D에서는 light가 없어도 화면이 보인다. (3D의 경우 보이지 않음)
  2D게임이 가벼운 이유중에는 빛을 사용하지 않아서도 있음.
* hierachy > camera에서 배경색 설정 가능. 
* 스프라이트 : 2d 그래픽 object이다.    
  만드는 방법은 hierachy 판에서 우클릭 > 2d object > sprite 에서 만들 수 있다.    
  SpriteRenderer : 스프라이트를 보여주는 컴포넌트(sprite색상 변경 가능) 
* camera > size의 값을 조절하면 zoom in/out을 할 수 있다. (값이 작으면 zoom in)!
* 좌측 상단에 십자화살표 모양 누르고 카메라 움직이면 카메라를 움직일 수 있다. 
* 2D지만 x,y,z 축 다 존재는한다. 그러나 z축 변화는 보기에 반영되지 않는다.(이유 : main camera가 orhotgraphic projection으로 적용하고 있기 때문이다. projection으로 perspective를 바꾸면 당연히 z축 변화에 따라 화면에서 보는 것도 달라진다. 사영역시 camera에서 변경 가능하다.)
* 두개의 sprite가 겹칠때, 누가 더 앞에 보여지게 하느냐를 정하는 방법은    
  * z축으로 결정
  * order in layer를 사용하는 방법 : spriteRenderer에서 order in layer를 변경해준다. 클수록 앞으로온다.
* asset > 이건 소스를 모아두는 공간 같은거야(flutter의 asset이랑 같은 의미) 여기어 내가 만든 2d sprite이미지같은거 넣어두기.
* 생성된 sprite의 spriteRenderer에서 color위에 있는 sprite를 선택하면 다른것으로 바꿀 수 있다.(내가 넣은 그림같은걸로 변경 가능) 
## 물리 적용
* box Collider 2D 를 각 sprite에 적용하기 
* player한테는 rigied body 2d도 적용하기  
* defailt contact offset : 충돌여백. collider을 딱 맞게 적용해도 사이에 간격이 생기는 경우 이걸 0으로 만들어주기. edit>project setting > defailt contact offset
