\ blinky for Mecrisp Quintus on Longan Nano ( RISC-V chip GD32FV103 )

: b. binary . decimal ;


\ taken from datasheet GD32VF103_User_Manual_EN_V1.0.pdf

%0010 constant output-low-freq

$40010800 constant GPIOA 

$40011000 constant GPIOC 

$C constant OCTL-offset


\ taken from Longan Nano datasheet

: red-led GPIOC 13 ;

: green-led GPIOA 1 ;

: blue-led GPIOA 2 ;



\ shift 4 bit control bits to whole register mask
 
: shift-to-pin ( bits pin --- mask ) 4 * lshift ;


\ if pin > 7, use high register

: transform-high-pin ( gpio-port pin -- gpio-port pin )

  dup 8 >= if 8 - swap $4 + swap then ;


\ set gpio pin mode

: gpio-set-mode ( mask gpio-port pin -- )
 
  transform-high-pin ( mask gpio-port pin )

  swap dup @ ( mask pin gpio-port old-val )

  2 pick %1111 swap shift-to-pin ( mask pin gpio-port oldval mask-to-clear ) 

  bic 2swap ( gpio-port cleared-val mask pin )

  shift-to-pin or ( gpio-port new-val )

  swap ! ( -- )

  ; 

: toggle ( gpio pin -- )

  %1 swap lshift swap OCTL-offset + xor! ;
  
  

\ setup - first set to high, then change mode

red-led toggle

green-led toggle

blue-led toggle

output-low-freq red-led gpio-set-mode

output-low-freq green-led gpio-set-mode

output-low-freq blue-led gpio-set-mode


\ some funnky lights


: delay 900000 0 do loop ;

 
: blink		

do

green-led toggle

delay

green-led toggle

red-led toggle

delay

red-led toggle

blue-led toggle

delay

blue-led toggle

delay

loop

;	


3 0 blink


: funk begin toggle delay depth 1 <= until ;


red-led blue-led red-led green-led red-led blue-led 

red-led blue-led red-led green-led red-led blue-led 

red-led blue-led red-led green-led red-led blue-led funk





