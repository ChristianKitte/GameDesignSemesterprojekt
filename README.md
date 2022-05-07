![image](https://user-images.githubusercontent.com/32162305/150810942-99672aac-99af-47ea-849b-ba263fae0c3f.png)

---

**Game Design**

**Dozent: Hans-Georg Reimer (Beuth Hochschule für Technik Berlin)**

**Studiengang Medieninformatik Online MA, Sommersemester 2022**

**University of Applied Sciences Emden/Leer, Faculty of Technology, Department of Electrical Engineering and Informatics**

---

# Studienarbeit Game Development: Entwicklung eines 3D Spiels (Prototyp)
Als Semesteraufgabe soll ein 3D Spiel mit festgelegten Anforderungen prototypisch umgesetzt werden. Lediglich
das erste Level muss spielbar sein. 

Der aktuelle und zum aktuellen Zeitpunkt sehr pototypische Stand ist als WebGL Anwendung
[hier](https://gamedev.ckitte.de/) 
aufrufbar.

## Kurzbeschreibung

Jeder kennt die Minions: klein, gelb und immer auf die Suche nach dem einen, den wahren Meister
und Schurken. Mit Gru könnte alles so schön sein, wenn da nicht der Heißhunger auf Bananen währe.
Sehen sie eine Banane, so ist das Chaos vorprogrammiert.


Nun ist es Gru zu bunt geworden. Kurzerhand hat er alle Bananen, die er finden konnte an einen
sicheren Ort versteckt und den Zugang durch ein fieses lebendes Labyrinth der bewegenden Mauern
versperrt. Aber nicht nur dass: Zusätzlich nutzt er seine neueste Erfindung: künstliche Geister, die
einen verfolgen und kostbare Lebenspunkte stehlen. So kam es zur Legende des Bananenbaums.
Hier kommt Hugo ins Spiel. Hugo ist ein an sich sehr durchschnittlicher und obendrauf kleiner
Minion. Aber er hat eine Vision, ein Ziel, eine Mission: Er wird den großen Bananenbaum am Ende
des Labyrinths finden und alle seine Freunde in das gelobte Land führen, wo die Bananen in der Luft
hängen.


Aber der Weg ist schwer und gefährlich. Level um Level muss er durchqueren, um zu seinem Ziel zu
gelangen und sein Vorrat an Lebenspunkten ist wie seine Zeit begrenzt. Nicht nur droht ihm hierbei
ständig Gefahr durch die Kollision mit umherschwebenden Hindernissen, sondern auch durch
Geister. Diese Plagegeister heften sich an Ihm, sobald er in ihre Nähe kommt, und verfolgen ihn. Nur
wenn er Abstand zu ihnen hält, kann er seine Lebenspunkte retten und nicht einschlafen. Nur gut,
dass er seinen Vorrat auffrischen und Hilfe finden kann.


Zu Anfang scheint der Weg einfach, aber je näher er sein Ziel kommt, umso größere Probleme muss
er überwinden. Sehen wir, wie weit er kommt

## Gameplay

Auf einem grundsätzlich gleichbleibenden Spielfeld muss der Spieler von einem willkürlichen
Startpunkt zu einem sichtbaren Zielpunkt gelangen. Hierfür hat er nur eine gewisse Zeitspanne zur
Verfügung. Erreicht er das Ziel in dieser Zeitspanne, fängt das nächste und schwerere Level an. Der
Spieler startet an einer neuen, zufälligen Position.
Zu Anfang des Spieles verfügt der Spieler über eine anfängliche Zahl an Lebenspunkten. Durch
bestimmte Aktionen können weitere Lebenspunkte gewonnen oder verloren werden. Bei Erreichen
eines höheren Levels werden die aktuellen Punkte mitgenommen, bei Beginn eines niedrigerenGame Design Dokument


Levels diese wieder auf einen Anfangsstand gesetzt. Das jeweils aktuelle Level ist verloren, wenn der
Spieler über keine Lebenspunkte mehr verfügt.
Erreicht der Spieler den Zielpunkt nicht in der vorgegebenen Zeit oder hat er keine Lebenspunkte
mehr, so hat er das aktuelle Level verloren und betritt das vorhergehende Level an einen zufälligen
Ort. Hierbei werden seine Lebenspunkte wieder auf einen Anfangszustand gesetzt. Wird der erste
und einfachste Level verloren, so hat er das Spiel als Ganzes verloren. Gewinnt der Spieler das
aktuelle Level, indem er mit Lebenspunkten und innerhalb der vorgegebenen Zeit das Ziel erreicht,
so betritt er an einen zufälligen Ort mit seinen bis dahin erreichten Punktestand das nächsthöhere
Level.


Auf dem Weg dorthin kann er visuell sichtbare Zwischenziele anlaufen. Das Erreichen eines
Zwischenziels wird mit der angezeigten Menge an Lebenspunkten, einer größeren Widerstandskraft
oder Schnelligkeit belohnt. Die Ziele können anhand ihrer Erscheinung unterschieden und nur
einmalig genutzt werden. Ihre Anzahl und die maximal mögliche Menge an Lebenspunkten ist
antiproportional zum erreichten Level.


Bei der Erfüllung der Anforderung stehen dem Spieler sich geradlinig bewegende Hindernisse im
Weg. Diese kommen aus zufälligen Richtungen aus den Wänden und bewegen sich orthogonal zur
gegenüberliegenden Wand der Spielfeldbegrenzung. Hierbei ist deren Anzahl, Geschwindigkeit,
Höhe und Breite zufallsbasiert gesteuert. Die maximale Anzahl und Geschwindigkeit sind hierbei
proportional zum erreichten Level.


Der Spieler kann diese Hindernisse umlaufen, überspringen oder darunter seinen Weg wählen.
Kollidiert der Spieler jedoch mit einem Hindernis, so verliert er Lebenspunkte und wird an einen
zufällig gewählten Ort des Spielfeldes teleportiert. Die Anzahl verlorener Lebenspunkte ist hierbei
proportional zum erreichten Level.
Ein weiterer Widerstand ist in Gestalt von Geistern gegeben. Geister sind zufällig positionierte NLPs
mit einer gewissen Intelligenz. Kommt ein Spieler in die Nähe eines Geistes, so wird der Geist sich in
Richtung des Spielers bewegen und ihn verfolgen. Hierbei kann er durch alle Hindernisse durchgehen
und sich somit geradlinig bewegen.


Erreicht ein Geist den Spieler, so verliert dieser in konstanter Folge Lebenspunkte. Dem Geist kann
entkommen werden, sobald eine gewisse Distanz zwischen Geist und Spieler geschaffen wird. Mit
zunehmendem Level wird die Anzahl der Geister höher und der von ihnen angerichtete Schaden
größer.


Beendet ein Spieler ein Level und will nicht weiterspielen, so kann es sich in einer Bestenliste mit
einem frei wählbaren Namen eintragen, sofern er zu den 15 besten Spielern gehört
