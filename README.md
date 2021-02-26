# Interpreter

This is a C# implementation of the following guide found [here](https://ruslanspivak.com/lsbasi-part1/)

Listing 1 - Example program
~~~ pascal
PROGRAM Part12;
VAR
   a : INTEGER;

PROCEDURE P1;
VAR
   a : REAL;
   k : INTEGER;

   PROCEDURE P2;
   VAR
      a, z : INTEGER;
   BEGIN {P2}
      z := 777;
   END;  {P2}

BEGIN {P1}

END;  {P1}

BEGIN {Part12}
   a := 10;
END.  {Part12}
~~~

Listing 2 - Example program
``` pascal
PROGRAM Part11;
VAR
   number : INTEGER;
   a, b   : INTEGER;
   y, pi  : REAL;

BEGIN {Part11}
   number := 2;
   pi := 3.1459;
   a := number ;
   b := 10 * a + 10 * number DIV 4;
   y := 20 / 7 + pi;
END.  {Part11}
```