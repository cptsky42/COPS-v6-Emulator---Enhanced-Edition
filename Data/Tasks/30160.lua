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

function processTask30160(client, idx)
    name = "Stoneman"
    face = 1

    if (idx == 0) then

        text(client, "I am issuing all kinds of ore vouchers. What Vouchers do you want?")
        link(client, "Copper Ore Voucher", 1)
        link(client, "Ore Voucher", 2)
        pic(client, 28)
        create(client)

    elseif (idx == 1) then

        text(client, "To obtain a Copper Ore Voucher, 12 Copper Ores will be charged. Are you ready for that?")
        link(client, "Yeah. Here you are.", 3)
        link(client, "No. I shall come later.", 255)
        pic(client, 28)
        create(client)

    elseif (idx == 2) then

        text(client, "To obtain an Ore Voucher, 10 Copper Ore Vouchers are required. Are you ready for that?")
        link(client, "Yeah. Here you are.", 4)
        link(client, "No. I shall come later.", 255)
        pic(client, 28)
        create(client)

    elseif (idx == 3) then

        if hasItems(client, 1072020, 1072029, 12) then

            if spendItems(client, 1072020, 1072029, 12) then

                awardItem(client, "721175", 1)

            else

                text(client, "Sorry, you do not have 12 Copper Ores.")
                link(client, "I see.", 255)
                pic(client, 28)
                create(client)

            end

        else

            text(client, "Sorry, you do not have 12 Copper Ores.")
            link(client, "I see.", 255)
            pic(client, 28)
            create(client)

        end

    elseif (idx == 4) then

        if hasItem(client, 721175, 10) then

            if spendItem(client, 721175, 10) then

                awardItem(client, "721176", 1)

            else

                text(client, "Sorry, you do not have 10 Copper Ore Vouchers.")
                link(client, "I see.", 255)
                pic(client, 28)
                create(client)

            end

        else

            text(client, "Sorry, you do not have 10 Copper Ore Vouchers.")
            link(client, "I see.", 255)
            pic(client, 28)
            create(client)

        end

    end

end
