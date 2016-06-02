--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:51 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30168(client, idx)
    name = "Yougo"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721250, 1) then

            text(client, "What else can I do for you?")
            link(client, "No, thanks.", 255)
            pic(client, 62)
            create(client)

        else

            if hasItem(client, 721253, 1) then

                text(client, "Roy is really smart. I will tell you the truth. It is me who killed the dartists and stole the jade.")
                link(client, "Surrender your PeaceJade.", 1)
                pic(client, 62)
                create(client)

            else

                text(client, "Sigh, when can I get back the Kylin Jade?")
                link(client, "Is JadeKylin valuable?", 255)
                pic(client, 62)
                create(client)

            end

        end

    elseif (idx == 1) then

        text(client, "But BladeGhost stole my Kylin Jade and told me that if I killed the bodyguards and got the jade, he will return the KylinJade.")
        link(client, "I will get it for you.", 2)
        pic(client, 62)
        create(client)

    elseif (idx == 2) then

        text(client, "If you can find out the Kylin Jade for me, I will surrender.")
        link(client, "Here is the Kylin Jade.", 3)
        link(client, "I will find it soon.", 255)
        pic(client, 62)
        create(client)

    elseif (idx == 3) then

        if hasItem(client, 721254, 1) then

            spendItem(client, 721253, 1)
            spendItem(client, 721254, 1)
            awardItem(client, "721250", 1)
            text(client, "Thank you for finding the KylinJade back for me. Now take the PeaceJade to EscortChief.")
            text(client, "Consider my earnest resipiscence, please let me go home for a few days, is it ok?")
            link(client, "It is ok.", 255)
            pic(client, 62)
            create(client)

        else

            text(client, "You are kidding. Didn`t you say that you have taken the Kylin Jade with you?")
            link(client, "I will get it for you.", 255)
            pic(client, 62)
            create(client)

        end

    end

end
