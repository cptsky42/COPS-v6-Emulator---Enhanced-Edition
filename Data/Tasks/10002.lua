--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:40 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10002(client, idx)
    name = "Barber"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Now I can offer two types of hairstyles: New styles and nostalgic styles. Would you like to cost 500 silvers to make a change?")
            link(client, "New styles.", 1)
            link(client, "Nostalgic styles.", 2)
            link(client, "Keep my current style.", 255)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Which style would you like to select from?")
            link(client, "New HairStyle01", 3)
            link(client, "New HairStyle02", 4)
            link(client, "New HairStyle03", 5)
            link(client, "New HairStyle04", 6)
            link(client, "New HairStyle05", 7)
            link(client, "New HairStyle06", 8)
            link(client, "New HairStyle07", 9)
            link(client, "Next.", 10)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 2) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Which style would you like to select from?")
            link(client, "Nostalgic01", 11)
            link(client, "Nostalgic02", 12)
            link(client, "Nostalgic03", 13)
            link(client, "Nostalgic04", 14)
            link(client, "Nostalgic05", 15)
            link(client, "Nostalgic06", 16)
            link(client, "Bald head", 17)
            link(client, "I chanded my mind.", 255)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 3) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 30)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool!", 255)
                link(client, "I want to change it.", 18)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 4) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 33)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool!", 255)
                link(client, "I want to change it.", 18)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 5) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 34)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool!", 255)
                link(client, "I want to change it.", 18)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 6) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 35)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool!", 255)
                link(client, "I want to change it.", 18)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 7) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 36)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool!", 255)
                link(client, "I want to change it.", 18)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 36)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool!", 255)
                link(client, "I want to change it.", 18)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 8) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 37)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool!", 255)
                link(client, "I want to change it.", 18)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 9) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 38)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool!", 255)
                link(client, "I want to change it.", 18)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 10) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Which style would you like to select from?")
            link(client, "New HairStyle08", 19)
            link(client, "New HairStyle09", 20)
            link(client, "Previous.", 1)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 11) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 10)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool.", 255)
                link(client, "I want to change it.", 21)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 12) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 11)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool.", 255)
                link(client, "I want to change it.", 21)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 13) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 12)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool.", 255)
                link(client, "I want to change it.", 21)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 14) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 13)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool.", 255)
                link(client, "I want to change it.", 21)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 15) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 14)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool.", 255)
                link(client, "I want to change it.", 21)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 16) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 15)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool.", 255)
                link(client, "I want to change it.", 21)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 17) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 0)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool.", 255)
                link(client, "I want to change it.", 21)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 18) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Which style would you like to change to?")
            link(client, "New HairStyle01", 22)
            link(client, "New HairStyle02", 23)
            link(client, "New HairStyle03", 24)
            link(client, "New HairStyle04", 25)
            link(client, "New HairStyle05", 26)
            link(client, "New HairStyle06", 27)
            link(client, "New HairStyle07", 28)
            link(client, "Next.", 29)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 19) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 39)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool!", 255)
                link(client, "I want to change it.", 18)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 20) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getMoney(client) < 500 then

                text(client, "You have not enough money.")
                link(client, "I`m sorry.", 255)
                pic(client, 111)
                create(client)

            else

                spendMoney(client, 500)
                setHair(client, getHair(client) - (getHair(client) % 100) + 40)
                play(client, "sound/gethp.wav", false)
                broadcastEffect(client, "Health")
                text(client, "It`s completed. Are you satisfied with your new hairstyle?")
                link(client, "Cool!", 255)
                link(client, "I want to change it.", 18)
                pic(client, 111)
                create(client)

            end

        end

    elseif (idx == 21) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Which style would you like to change to?")
            link(client, "Nostalgic01", 30)
            link(client, "Nostalgic02", 31)
            link(client, "Nostalgic03", 32)
            link(client, "Nostalgic04", 33)
            link(client, "Nostalgic05", 34)
            link(client, "Nostalgic06", 35)
            link(client, "Bald head", 36)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 22) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 30)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool!", 255)
            link(client, "I want to change it.", 18)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 23) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 33)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool!", 255)
            link(client, "I want to change it.", 18)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 24) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 34)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool!", 255)
            link(client, "I want to change it.", 18)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 25) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 35)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool!", 255)
            link(client, "I want to change it.", 18)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 26) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 36)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool!", 255)
            link(client, "I want to change it.", 18)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 27) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 37)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool!", 255)
            link(client, "I want to change it.", 18)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 28) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 38)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool!", 255)
            link(client, "I want to change it.", 18)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 29) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Which style would you like to change to?")
            link(client, "New HairStyle08", 37)
            link(client, "New HairStyle09", 38)
            link(client, "Previous.", 18)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 30) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 10)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool.", 255)
            link(client, "I want to change it.", 21)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 31) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 11)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool.", 255)
            link(client, "I want to change it.", 21)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 32) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 12)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool.", 255)
            link(client, "I want to change it.", 21)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 33) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 13)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool.", 255)
            link(client, "I want to change it.", 21)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 34) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 14)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool.", 255)
            link(client, "I want to change it.", 21)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 35) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 15)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool.", 255)
            link(client, "I want to change it.", 21)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 36) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 0)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool.", 255)
            link(client, "I want to change it.", 21)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 37) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 39)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool!", 255)
            link(client, "I want to change it.", 18)
            pic(client, 111)
            create(client)

        end

    elseif (idx == 38) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            setHair(client, getHair(client) - (getHair(client) % 100) + 40)
            play(client, "sound/gethp.wav", false)
            broadcastEffect(client, "Health")
            text(client, "It`s completed. Are you satisfied with your new hairstyle?")
            link(client, "Cool!", 255)
            link(client, "I want to change it.", 18)
            pic(client, 111)
            create(client)

        end

    end

end
