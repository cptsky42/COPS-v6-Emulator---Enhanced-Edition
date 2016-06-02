--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:52 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask300655(client, idx)
    name = "OldExplorer"
    face = 1

    if (idx == 0) then

        text(client, "This is the way to the meteor and adventure area. The main monsters in adventure area are ferocious but may drop nice")
        text(client, "items. In the two areas, special monsters have the same appearance and name as normal ones, but they may drop")
        text(client, "meteors or DragonBalls.You can`t use teleport scrolls there. You may ask the boatman on the in-island to help you leave there.")
        link(client, "I want to have a try.", 1)
        link(client, "Thanks.", 255)
        pic(client, 12)
        create(client)

    elseif (idx == 1) then

        setRecordPos(client, 1210, 1039, 718)
        move(client, 1210, 1039, 718)

    end

end
