![image](https://user-images.githubusercontent.com/32162305/150810942-99672aac-99af-47ea-849b-ba263fae0c3f.png)

---

**Game Design**

**Dozent: Hans-Georg Reimer (Beuth Hochschule für Technik Berlin)**

**Studiengang Medieninformatik Online MA, Sommersemester 2022**

**University of Applied Sciences Emden/Leer, Faculty of Technology, Department of Electrical Engineering and Informatics**

---

Als Semesteraufgabe soll im Modul Game Design ein 3D Spiel mit festgelegten Anforderungen prototypisch umgesetzt werden. Lediglich das erste Level muss spielbar sein.

Das Game Design Dokument kann [hier](https://github.com/ChristianKitte/GameDesignSemesterprojekt/blob/main/GDDundPr%C3%A4sentation/Game%20Design%20Dokument_20220712.pdf) gefunden werden.

Alle für die Ausführung notwendigen Daten finden sich im Ordner [OutputWebGL](https://github.com/ChristianKitte/GameDesignSemesterprojekt/tree/main/OutputWebGL).

Eine spielbare Version findet sich [hier](https://gamedev.ckitte.de/).

Die hier hinterlegten Daten können unter Angabe der Quelle für den eigenen, nicht kommerziellen Gebrauch verwendet werden. Einige der eingesetzten Assets unterliegen eigenen Restriktionen. Details hierzu finden sich im [GGD](https://github.com/ChristianKitte/GameDesignSemesterprojekt/blob/main/GDDundPr%C3%A4sentation/Game%20Design%20Dokument_20220712.pdf). Alle Rechte verbleiben beim Autor.

## Legende des Bananenbaums

Jeder kennt die Minions: klein, gelb und immer auf die Suche nach dem einen, den wahren Meister und Schurken. Mit Gru könnte alles so schön sein, wenn da nicht der Heißhunger auf Bananen wäre. Sehen sie eine Banane, so ist das Chaos vorprogrammiert.

Nun ist es Gru zu bunt geworden. Kurzerhand hat er alle Bananen, die er finden konnte an einem sicheren Ort versteckt und den Zugang durch ein fieses, lebendiges Labyrinth der bewegenden Mauern versperrt. Bei jeder Kollision mit Ihnen verliert er einige Bananen. Aber nicht nur dass: Zusätzlich nutzt Gru seine neueste Erfindung: künstliche Geister, die einen verfolgen und alle gesammelten Bananen stehlen wollen. So kam es zur Legende des Bananenbaums.

Hier kommt Hugo ins Spiel. Hugo ist ein an sich sehr durchschnittlicher und obendrauf kleiner Minion. Aber er hat eine Vision, ein Ziel, eine Mission: Er wird den großen Bananenbaum am Ende des Labyrinths finden und alle seine Freunde in das gelobte Land führen, wo die Bananen in der Luft hängen.

Hierfür muss er der Spur der goldenen Banane folgen. Nur wenn er diese rechtzeitig findet und mindestens eine Banane dabei hat, kann er die nächste Ebene erreichen. Aber der Weg ist schwer und gefährlich. Ebene um Ebene muss er durchqueren, um zu seinem Ziel zu gelangen und sein Vorrat an Bananen ist wie seine Zeit begrenzt.

Nur gut, dass genug Bananen rumliegen, die er aufsammeln kann. Kürbisse und kleine Wänden bieten eine Zeitlang Schutz vor den Wänden und Geistern. Es ist nicht einfach, sie aufzusammeln. Sie Schweben und verschwinden im Gras um anschließend hoch aufzusteigen. Springe, um sie zu erreichen.

Zu Anfang scheint der Weg einfach, aber je näher er sein Ziel kommt, umso größere Probleme muss er überwinden. Sehen wir, wie weit er kommt.

## Gameplay

Auf einem grundsätzlich gleichbleibenden, durch hohe Mauern umrandeten Spielfeld muss der Spieler von einem willkürlichen Startpunkt zu einem nicht zwingend sofort sichtbaren Zielpunkt (finale Levelziel, „Goldene Banane“) gelangen. Hierfür hat er nur eine gewisse Zeitspanne zur Verfügung.

Erreicht er das Ziel in dieser Zeitspanne mit mindestens einer Banane, fängt das nächste und schwerere Level an. Der Spieler startet an einer neuen, zufälligen Position, behält jedoch seine gesammelten Bananen. Wird eine der Vorgaben nicht erfüllt, so wird das Spiel im nächsttieferen Level fortgesetzt. Hierbei wird sein Bestand an Bananen auf null gesetzt. Wird der erste und einfachste Level verloren, so wird dieser wiederholt. Insofern kann das Spiel an sich nicht verloren werden.  

Zu Anfang des Spieles (erster Level, Startlevel) verfügt der Spieler über keine Bananen. Durch Überlaufen oder Anspringen der auf dem Spielfeld verteilten Bananen ist er jedoch in der Lage, seinen Vorrat zu vergrößern. Jede Banane kann nur einmal eingesammelt werden und verschwindet hierdurch. Neben Bananen existieren weitere Objekte (Zwischenziele). Zwischenziele vom Typ „Wandsegment“ bieten Schutz vor den „Wandernden Wänden“, „Halloween Kürbisse“ vor Geister. Dieser Schutz gilt jedoch nur für eine gewisse Anzahl an Sekunden. Während dieser Zeit ist der Spieler für Geister unsichtbar und eine Kollision mit ihnen oder einer „Wandernden Wand“ bleibt ohne Folgen. Zwischenziele können analog zu Bananen durch Überrennen oder Anspringen einmalig eingesammelt werden.

Sowohl Bananen als auch Zwischenziele verharren während eines Levels an ihrer Position, ändern jedoch beständig ihre Höhe. Hierbei verschwinden sie abwechselnd im Boden und sind für den Spieler nicht oder nur schwer sichtbar, oder schweben in eine für den Spieler nicht erreichbaren Höhe.  

Im Verlauf des Spiels wirken der Erfüllung der Aufgaben zwei Hindernisse entgegen. Bei den „Wandernden Wänden“ handelt es sich um Wandstücke, die aus den umgebenen Wänden des Spielfeldes erscheinen und sich gradlinig zur gegenüberliegenden Wand bewegen und dort verschwinden. Bei der Kollision des Spielers mit den Wänden, verliert dieser einen Teil der eingesammelten Bananen. Verfügt er über keine Bananen, so wird dessen Bestand trotzdem um die Zahl reduziert. Um das Level erfolgreich zu beenden, muss dieser Bestand zunächst ausgeglichen werden.  

Ein weiteres Hindernis bilden Geister. Geister sind zufällig positionierte NLPs mit einer gewissen „Intelligenz“. Kommt ein Spieler in die Nähe eines Geistes, so wird der Geist sich in Richtung des Spielers bewegen und ihn verfolgen. Hierbei wird seine Wahrnehmung und Fortbewegung durch keine Hindernisse behindert. Erreicht ein Geist den Spieler, so verliert dieser in konstanter Folge Bananen. Dem Geist kann entkommen werden, wenn eine gewisse Distanz zwischen Geist und Spieler geschaffen wird.  

Mit jedem erfolgreich absolvierten Level ist es schwieriger, das Ziel zu erreichen, da tendenziell immer weniger Objekte vom Typ „Banane“ mit immer kleineren Werten vorhanden sind. Gleichzeitig wird sowohl die Anzahl an „Halloween Kürbissen“ und „Wandfragmenten“ sowie deren Schutzwirkungen immer kleiner. Eine weitere Schwierigkeit stellen die zunehmend größeren und schädlicheren „Wandernden Wände“ und sensiblere Geister da.
