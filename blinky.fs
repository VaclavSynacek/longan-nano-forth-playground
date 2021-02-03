\ Program Name: blinky-1.fs  for Mecrisp-Stellaris by Matthias Koch
\ This program blinks a green led, it's the simplest of 'blinkies' 
\ Hardware: STM32F0 Discovery board
\ Author:  t.porter <terry@tjporter.com.au>


: b. binary . decimal ;


\ output, push-pull

%0010 constant output

%1111 constant all


: shift-to-pin ( bits pin --- mask ) 4 * lshift ;

$40010800 constant GPIOA-base 

$40011000 constant GPIOC-base 

: set-gpio-control ( gpio-port pin mask -- ) swap shift-to-pin swap bis! ; 

GPIOA-base 2 output set-gpio-control

GPIOA-base 1 output set-gpio-control

$c constant control-offset

%1 1 lshift GPIOA-base control-offset + xor!

%1 2 lshift GPIOA-base control-offset + xor!

GPIOA-base control-offset + @ b.

\ works above to set to set output and then toggle green and blue leds
\ scrap underneat
\ ******************************************************************

: set-blue-output mode-output 4 2 * lshift GPIOA-base bis! ;

set-blue-output

mode-output 4 2 * lshift binary . decimal

GPIOA-base @ binary . decimal


%01  18 lshift $48000800 bis!		

: half-second-delay 400000 0 do loop ;

: green-led.on   %1  9 lshift $48000818 + . ;	

: green-led.off  %1 9 lshift $48000828 + . ; 
 
: blink		

do

green-led.on

half-second-delay

green-led.off

half-second-delay

loop

;	


3 0 blink
