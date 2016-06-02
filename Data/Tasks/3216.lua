--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:28 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask3216(client, idx)
    name = "Mr.Free"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if hasItem(client, 723087, 1) then

                text(client, "I love living a seclusive life. Don`t bother me!")
                link(client, "I heard that you need LuckyAmulets.", 1)
                pic(client, 74)
                create(client)

            else

                text(client, "I love living a seclusive life. Don`t bother me!")
                link(client, "I`ll leave now.", 255)
                pic(client, 74)
                create(client)

            end

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Yes. My only hobby is to collect LuckyAmulets.")
            link(client, "I happen to have one.", 2)
            link(client, "I`d better keep it.", 255)
            pic(client, 74)
            create(client)

        end

    elseif (idx == 2) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Great! If you present LuckyAmulet to me, I`ll make a unique hairstyle for you.")
            link(client, "Thank you.", 3)
            link(client, "I`d better keep it.", 255)
            pic(client, 74)
            create(client)

        end

    elseif (idx == 3) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getSex(client) == 1 then

                text(client, "Thank you very much. Which hairstyle would you like to select from?")
                link(client, "Dignified one.", 4)
                link(client, "Unruly one.", 5)
                link(client, "Staid one.", 6)
                link(client, "Cool one.", 7)
                link(client, "Handsome one.", 8)
                pic(client, 74)
                create(client)

            else

                text(client, "Thank you very much. Which hairstyle would you like to select from?")
                link(client, "Lovely one.", 4)
                link(client, "Comely one.", 5)
                link(client, "Pretty one.", 6)
                link(client, "Elegant one.", 7)
                link(client, "Nifty one.", 8)
                pic(client, 74)
                create(client)

            end

        end

    elseif (idx == 4) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            spendItem(client, 723087, 1)
            setHair(client, getHair(client) - (getHair(client) % 100) + 21)
            text(client, "Are you satisfied with your new hairsyle?")
            link(client, "Great! Thank you.", 255)
            link(client, "I`d like to try another one.", 9)
            pic(client, 74)
            create(client)

        end

    elseif (idx == 5) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            spendItem(client, 723087, 1)
            setHair(client, getHair(client) - (getHair(client) % 100) + 22)
            text(client, "Are you satisfied with your new hairsyle?")
            link(client, "Great! Thank you.", 255)
            link(client, "I`d like to try another one.", 9)
            pic(client, 74)
            create(client)

        end

    elseif (idx == 6) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            spendItem(client, 723087, 1)
            setHair(client, getHair(client) - (getHair(client) % 100) + 23)
            text(client, "Are you satisfied with your new hairsyle?")
            link(client, "Great! Thank you.", 255)
            link(client, "I`d like to try another one.", 9)
            pic(client, 74)
            create(client)

        end

    elseif (idx == 7) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            spendItem(client, 723087, 1)
            setHair(client, getHair(client) - (getHair(client) % 100) + 24)
            text(client, "Are you satisfied with your new hairsyle?")
            link(client, "Great! Thank you.", 255)
            link(client, "I`d like to try another one.", 9)
            pic(client, 74)
            create(client)

        end

    elseif (idx == 8) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            spendItem(client, 723087, 1)
            setHair(client, getHair(client) - (getHair(client) % 100) + 25)
            text(client, "Are you satisfied with your new hairsyle?")
            link(client, "Great! Thank you.", 255)
            link(client, "I`d like to try another one.", 9)
            pic(client, 74)
            create(client)

        end

    elseif (idx == 9) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getSex(client) == 1 then

                text(client, "Which hairstyle would you like to select from?")
                link(client, "Dignified one.", 10)
                link(client, "Unruly one.", 11)
                link(client, "Staid one.", 12)
                link(client, "Cool one.", 13)
                link(client, "Handsome one.", 14)
                pic(client, 74)
                create(client)

            else

                text(client, "Which hairstyle would you like to select from?")
                link(client, "Lovely one.", 10)
                link(client, "Comely one.", 11)
                link(client, "Pretty one.", 12)
                link(client, "Elegant one.", 13)
                link(client, "Nifty one.", 14)
                pic(client, 74)
                create(client)

            end

        end

    elseif (idx == 10) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 21)
            text(client, "Are you satisfied with your new hairsyle?")
            link(client, "Great! Thank you.", 255)
            link(client, "I`d like to try another one.", 9)
            pic(client, 74)
            create(client)

        end

    elseif (idx == 11) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 22)
            text(client, "Are you satisfied with your new hairsyle?")
            link(client, "Great! Thank you.", 255)
            link(client, "I`d like to try another one.", 9)
            pic(client, 74)
            create(client)

        end

    elseif (idx == 12) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 23)
            text(client, "Are you satisfied with your new hairsyle?")
            link(client, "Great! Thank you.", 255)
            link(client, "I`d like to try another one.", 9)
            pic(client, 74)
            create(client)

        end

    elseif (idx == 13) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 24)
            text(client, "Are you satisfied with your new hairsyle?")
            link(client, "Great! Thank you.", 255)
            link(client, "I`d like to try another one.", 9)
            pic(client, 74)
            create(client)

        end

    elseif (idx == 14) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 25)
            text(client, "Are you satisfied with your new hairsyle?")
            link(client, "Great! Thank you.", 255)
            link(client, "I`d like to try another one.", 9)
            pic(client, 74)
            create(client)

        end

    end

end
