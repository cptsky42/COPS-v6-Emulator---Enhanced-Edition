--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:50 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30115(client, idx)
    name = "GreenSnake"
    face = 1

    if (idx == 0) then

        text(client, "Aha, What a beautiful scene. We are very lucky to have so many rare items.")
        link(client, "Devil, I must kill you.", 1)
        link(client, "Ah, I had better run away.", 255)
        pic(client, 110)
        create(client)

    elseif (idx == 1) then

        text(client, "If you spare my life, I will let you enter our territory. Once you break through the snake array, you may get our rare items.")
        link(client, "What is snake array?", 2)
        pic(client, 110)
        create(client)

    elseif (idx == 2) then

        text(client, "This battle array consists of 17 islands. Our boss guards the last one. If you can defeat our boss, you may get the rare items.")
        link(client, "I will spare your life.", 3)
        pic(client, 110)
        create(client)

    elseif (idx == 3) then

        text(client, "Many brave people have come for the rare item, but all are killed by us. Are you sure you want to challenge our battle array?")
        link(client, "I fear nothing.", 4)
        link(client, "I changed my mind.", 255)
        pic(client, 110)
        create(client)

    elseif (idx == 4) then

        move(client, 1063, 449, 358)
        setRecordPos(client, 1015, 717, 577)

    end

end
