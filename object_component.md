### 물제의 물리 요소

* rigidbody : 물리효과를 받게하기위한 필수 컴포넌트
  * mass : 무게 
  * kinematic : 물리 현상의 영향을 받지 않고, 스크립트를 통해서 이동시킬때만 영향을 받는다. 플레이어에 닿아도 움직이지 않는장애물들에 적용함.
* collider : 타 object와 충돌을 일으키지 않음. 
* default material : 오브젝트 재질 결정
  재질은 만들어서 사용할 수 있음. asset > create > material 만들고 오브젝트 인스팩터 창 하단에 드레그해서 넣음됨.
   * albedo : 이미지를 넣을 수 있음.
   * tiling : 택스쳐 반복 타일 개수.
* 물리 재질 : asset > create > physics material : 탄성과 마찰을 다루는 물리적인 재질. 오브젝트의 collider > material에 넣기
   * bounciness : 탄성 
   * bounciness combine : 다음 탄성을 계산하는 방식
   * friction : 마찰력. 낮을수록 많이 미끄러짐.
