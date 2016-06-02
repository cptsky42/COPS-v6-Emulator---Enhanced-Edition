--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:43 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask15140(client, idx)
    name = "Gouda"
    face = 1

    if (idx == 0) then

        text(client, "Kylin, a giant monster living in the jokul, is regarded as a holy creature. Its body is covered by the splendid squamas.")
        text(client, "It`s said that good luck will befall whoever gets the squama. The other day, I happened to get this rare squama.")
        text(client, "I dare not keep it to myself. So I`m here to tell you this exciting discovery.")
        link(client, "Please kindly tell me.", 1)
        link(client, "Forget it.", 255)
        pic(client, 54)
        create(client)

    elseif (idx == 1) then

        text(client, "Every night after 8 o`clock, twinkling spots will appear on the ground, it is the squama. Pick it up and you will get a")
        text(client, "secret present. But it depends on your luck to get better prize. You may get money, Meteor or a Dragon Ball. Wish you good luck")
        link(client, "Ok, thanks.", 255)
        pic(client, 54)
        create(client)

    end

end
