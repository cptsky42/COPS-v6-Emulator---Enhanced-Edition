--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:47 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30083(client, idx)
    name = "Killer"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "Invigorant") and (getMoney(client) >= 0) then

            text(client, "The drug is enough for me to go to Desert City! Thank you and take away the money. ")
            link(client, "Of course, I deserve it.", 1)
            pic(client, 9)
            create(client)

        elseif hasTaskItem(client, "ShopList") and (getMoney(client) >= 0) then

            text(client, "Please hurry up. I am afraid that I could not sustain any longer.")
            link(client, "Ok. Right back.", 255)
            pic(client, 9)
            create(client)

        else

         text(client, "Do not tell others you have seen me, or I must kill you.")
         link(client, "I see.", 255)
         link(client, "I never fear you.", 2)
         pic(client, 9)
         create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "Invigorant") and (getMoney(client) >= 0) then

            spendItem(client, 721119, 1)
            gainMoney(client, 2000)

        end

    elseif (idx == 2) then

        text(client, "You bear a murderous look on your face. We must be in the same line. I am in trouble. Will you help me?")
        link(client, "Tell me more details.", 3)
        link(client, "Sorry, I am too busy.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 3) then

        text(client, "I have killed too many people. I am wanted by the police. The patrol guard must kill me on sight.")
        link(client, "What can I do for you?", 4)
        pic(client, 9)
        create(client)

    elseif (idx == 4) then

        text(client, "I had run out of all Invigorant. Can you help me to take some from Druggist in TwinCity?")
        link(client, "It`s easy. Wait me.", 5)
        link(client, "It is so easy, why not ask others to help you?", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 5) then

        awardItem(client, "721118", 1)
        text(client, "Druggist are very busy and may be not in TwinCity. You should be hurry.")
        link(client, "Oh, I see.", 255)
        pic(client, 9)
        create(client)

    end

end
