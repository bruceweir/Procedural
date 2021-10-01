L-Systems

https://en.wikipedia.org/wiki/L-system
Lindenmayer system

An L-system consists of an alphabet of symbols that can be used to make strings, a collection of production rules that expand each symbol into some larger string of symbols, an initial "axiom" string from which to begin construction, and a mechanism for translating the generated strings into geometric structures.

http://www.kevs3d.co.uk/dev/lsystems/


Axiom could be a value derived from some parameter on a website....

A Grammar is the set of production rules.


-----------------------------------------------
Example:
Example 1: Algae
Lindenmayer's original L-system for modelling the growth of algae.

variables : A B
constants : none
axiom  : A
rules  : (A → AB), (B → A)
which produces:

n = 0 : A
n = 1 : AB
n = 2 : ABA
n = 3 : ABAAB
n = 4 : ABAABABA
n = 5 : ABAABABAABAAB
n = 6 : ABAABABAABAABABAABABA
n = 7 : ABAABABAABAABABAABABAABAABABAABAAB
Example 1: Algae, explained
n=0:               A             start (axiom/initiator)
                  / \
n=1:             A   B           the initial single A spawned into AB by rule (A → AB), rule (B → A) couldn't be applied
                /|     \
n=2:           A B      A        former string AB with all rules applied, A spawned into AB again, former B turned into A
             / | |       | \
n=3:         A B A       A B     note all A's producing a copy of themselves in the first place, then a B, which turns ...
           / | | | \     | \ \
n=4:       A B A A B     A B A   ... into an A one generation later, starting to spawn/repeat/recurse then



-------------------------------------------------

Algorithms for plant shapes
http://algorithmicbotany.org/papers/abop/abop-ch1.pdf

