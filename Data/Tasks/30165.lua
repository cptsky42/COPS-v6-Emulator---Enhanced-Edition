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

function processTask30165(client, idx)
    name = "EscortChief"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721250, 1) then

            text(client, "You find out PeaceJade at last. I will give you an AmbergrisBag in return.")
            link(client, "Ok, thank you.", 1)
            link(client, "Thanks. I do not need it.", 255)
            pic(client, 34)
            create(client)

        else

            if hasItem(client, 721253, 1) then

                text(client, "Hope you can find out PeaceJade soon.")
                link(client, "I will get it soon.", 255)
                pic(client, 34)
                create(client)

            else

                if hasItem(client, 721251, 1) then

                    text(client, "How is the case going on?")
                    link(client, "I will try my best.", 255)
                    pic(client, 34)
                    create(client)

                else

                    text(client, "Forty days` deadline is coming. I have no clue at all. What can I do?")
                    link(client, "What is wrong with you?", 2)
                    link(client, "I am just passing by.", 255)
                    pic(client, 34)
                    create(client)

                end

            end

        end

    elseif (idx == 1) then

        if spendItem(client, 721250, 1) then

            awardItem(client, "121127", 1)

        else

            text(client, "Don`t you take PeaceJade with you?")
            link(client, "I will bring it soon.", 255)
            pic(client, 34)
            create(client)

        end

    elseif (idx == 2) then

        text(client, "Last month, the government entrusted us to escort the PeaceJade. It is a great mission, so we deployed hundreds of bodyguards.")
        link(client, "And what was wrong?", 3)
        pic(client, 34)
        create(client)

    elseif (idx == 3) then

        text(client, "On the way to Desert City, all the bodyguards were killed and PeaceJade was stolen. But we have no clues of it at all.")
        link(client, "That is very bad.", 4)
        link(client, "I had better leave now.", 255)
        pic(client, 34)
        create(client)

    elseif (idx == 4) then

        text(client, "The government was furious about it. He commanded us to get it back in 40 days. Otherwise we`ll have to pay for it.")
        text(client, "Now, 30 days have passed and I still have no idea at all.")
        link(client, "Let me have a try.", 5)
        link(client, "Sigh, god bless you.", 255)
        pic(client, 34)
        create(client)

    elseif (idx == 5) then

        awardItem(client, "721251", 1)
        text(client, "That is very kind of you.")
        link(client, "You are welcome.", 255)
        pic(client, 34)
        create(client)

    end

end
