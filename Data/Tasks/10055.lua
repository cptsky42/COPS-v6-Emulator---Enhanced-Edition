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

function processTask10055(client, idx)
    name = "TaoistStar"
    face = 1

    if (idx == 0) then

        if getProfession(client) == 100 then

            text(client, "I can teach you Thunder and Cure. After you learn, you may cast Thunder to kill enemies, use Cure to heal yourself and others.")
            link(client, "I want to learn.", 1)
            link(client, "Just passing by.", 255)
            pic(client, 6)
            create(client)

        else

            text(client, "Sorry, you are not Taoist. I am here to teach Taoist some elementary spells.")
            link(client, "I see. Thanks.", 255)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 1) then

        if awardMagic(client, 1000, 0) then

            if awardMagic(client, 1005, 0) then

                text(client, "You have learned Thunder and Cure. Please remember that spells are only used to punish the devils and help the kind people.")
                link(client, "I see. Thanks.", 255)
                pic(client, 6)
                create(client)

            else

                text(client, "You have learned Thunder and Cure. Please remember that spells are only used to punish the devils and help the kind people.")
                link(client, "I see. Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        else

            if awardMagic(client, 1005, 0) then

                text(client, "You have learned Thunder and Cure. Please remember that spells are only used to punish the devils and help the kind people.")
                link(client, "I see. Thanks.", 255)
                pic(client, 6)
                create(client)

            else

                text(client, "You have learned Thunder and Cure. Please remember that spells are only used to punish the devils and help the kind people.")
                link(client, "I see. Thanks.", 255)
                pic(client, 6)
                create(client)

            end

        end

    end

end
