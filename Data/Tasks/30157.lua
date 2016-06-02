--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 4/1/2015 7:39:32 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30157(client, idx)
    name = "HouseAgent"
    face = 1

    if (idx == 0) then

        text(client, "I am the houseagent in Twin City. If you want to buy a house, you must have my authorization.")
        link(client, "Buy a house.", 1)
        link(client, "Upgrade my house.", 2)
        link(client, "Just passing by.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 1) then

        text(client, "You should make an effort to buy a house.")
        link(client, "What shall I do?", 3)
        link(client, "I changed my mind.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 2) then

        text(client, "Which class would you like to upgrade your house to?")
        link(client, "Second-class.", 4)
        link(client, "Let me think it over.", 255)
        pic(client, 28)
        create(client)

    elseif (idx == 3) then

        text(client, "If you can give me five Timber Voucher, I shall issue a House Permit to you.")
        link(client, "Here are the Vouchers.", 5)
        link(client, "I shall come later.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 4) then

        text(client, "Please exchange 10 Ore Vouchers for an Upgrade Certificate.")
        link(client, "Here are the vouchers.", 6)
        link(client, "I shall come later.", 255)
        pic(client, 28)
        create(client)

    elseif (idx == 5) then

        if hasItem(client, 721173, 5) then

            if spendItem(client, 721173, 5) then

                awardItem(client, "721170", 1)
                text(client, "Here is a House Permit. You may carry it to the market and ask the House Admin to sell a house to you.")
                link(client, "Thanks.", 255)
                pic(client, 92)
                create(client)

            else

                text(client, "Sorry, you do not have five Timber Vouchers.")
                link(client, "I see.", 255)
                pic(client, 92)
                create(client)

            end

        else

            text(client, "Sorry, you do not have five Timber Vouchers.")
            link(client, "I see.", 255)
            pic(client, 92)
            create(client)

        end

    elseif (idx == 6) then

        if hasItem(client, 721176, 10) then

            if spendItem(client, 721176, 10) then

                awardItem(client, "721174", 1)

            else

                text(client, "Sorry, you do not have 10 Ore Vouchers.")
                link(client, "I see.", 255)
                pic(client, 28)
                create(client)

            end

        else

            text(client, "Sorry, you do not have 10 Ore Vouchers.")
            link(client, "I see.", 255)
            pic(client, 28)
            create(client)

        end

    end

end
