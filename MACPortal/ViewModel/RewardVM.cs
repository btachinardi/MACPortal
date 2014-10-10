using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MACPortal.ViewModel
{
    public class RewardVM
    {
        public readonly List<RewardItemVM> Items;

        public List<RewardItemVM> GetTier(int tier)
        {
            return Items.Where(i => i.Tier == tier).ToList();
        }

        public List<RewardItemVM> GetRandom(int items)
        {
            var randomList = new List<RewardItemVM>();
            var random = new Random();
            var totalItems = Items.Count;
            var currentItem = 0;
            while (items > 0)
            {
                if (random.Next(totalItems - currentItem) < items)
                {
                    randomList.Add(Items[currentItem]);
                    items--;
                }
                currentItem++;
            }

            for (int i = randomList.Count; i > 1; i--)
            {
                var j = random.Next(i); 
                var tmp = randomList[j];
                randomList[j] = randomList[i - 1];
                randomList[i - 1] = tmp;
            }
            return randomList;
        }
        
        public RewardVM()
        {
            Items = new List<RewardItemVM>
            {
                    /**
                     * PRODUCTS TIER 1
                     */
                    #region Products Tier 1

                    new RewardItemVM(1, 1, 1)
                    {
                        Name = "Maquina fotográfica Digital",
                        Description = "Câmera Digital Branca 16.2 MP Sensor Exmor R CMOS, Wi-Fi, LCD de 2,7”, Zoom Óptico 8X, Filma em Full HD"
                    },
                    new RewardItemVM(1, 2 ,2)
                    {
                        Name = "Relogio Swatch",
                        Description = "Modelos Femininos e Masculinos"
                    },
                    new RewardItemVM(1, 4, 3)
                    {
                        Name = "Impressora Multifuncional + Calculadora Financeira",
                        Description = "Impressora Multifuncional (Imprime, copia e Digitaliza) Jato de Tinta Colorida com e-Print, Wireless Direct e Tela Sensível ao Toque + Calculadora Financeira."
                    },
                    new RewardItemVM(1, 5, 1)
                    {
                        Name = "Ipod Nano",
                        Description = "iPod Nano Apple. Com 5,4 mm de espessura e do tamanho de um cartão de crédito, o novo iPod nano é o iPod mais fino já criado. Com tela Multi-Touch de 2,5”. Cor mediante disponibilidade."
                    },
                    new RewardItemVM(1, 6, 3)
                    {
                        Name = "Blu-Ray + Box Star Wars Blu-Ray",
                        Description = "Blu-ray Player 3D Full HD com Wi-fi integrado; Mais Blu-ray Coleção Star Wars: A Saga Completa (9 Discos)"
                    },
                    new RewardItemVM(1, 7, 2)
                    {
                        Name = "Voucher vale roupa nas lojas Brooksfield Donna ou Brooksfield Man – R$ 800,00"
                    },
                    new RewardItemVM(1, 8, 1)
                    {
                        Name = "Forno de Microondas",
                        Description = "Forno de micro-ondas com Porta de Vidro Espelhada e Timer."
                    },
                    new RewardItemVM(1, 9, 1)
                    {
                        Name = "Tablet Samsung Galaxy",
                        Description = "Tablet Samsung Galaxy Tab 3 7.0 Wi-Fi Android 4.1 Processador Dual Core 1.2GHz Tela de 7.0” Câmera 3.0MP Branco."
                    },

                    #endregion

                    /**
                     * EXPERIENCES TIER 1
                     */
                    #region Experiences Tier 1

                    
                    new RewardExperienceVM(1, 1, 3)
                    {
                        Name = "Degustação no Restaurante e Chef do Ano | Esquina Mocotó (Para 4 Pessoas)",
                        Description = "Delicie-se com um menu degustação e bebidas na Esquina Mocotó nomeado restaurante do ano e comandado por Rodrigo Oliveira, também premiado como chef do ano pela Revista Veja. O menu vai além da gastronomia nordestida – especialidade do chef – e incorpora também influências de Minas e da região Norte. Depois de saborear as delícias elaboradas pelo famoso chef, leve uma típica cachaça para casa.",
                        Included = new List<object>
                            {
                                "Menu completo: petiscos, entrada, prato principal e sobremesa para 4 pessoas;",
                                "Bebidas: água, sucos, refrigerantes, vinhos, coquetéis e cervejas;",
                                "Taxa de serviço;",
                                "01 Cachaça de brinde."
                            },
                        NotIncluded = new List<object>
                            {
                                "Transporte até o hotel e local;",
                                "Estacionamento;",
                                "Qualquer item não mencionado como incluso."
                            },
                        Important = new List<object>
                            {
                                "Agendamento com 15 dias de antecedência;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos."
                            }
                    },

                    new RewardExperienceVM(1, 2, 4)
                    {
                        Name = "Day Use no Hotel Fazenda Campo dos Sonhos | Socorro (Para 4 Pessoas)",
                        Description = "Venha desfrutar de um dia maravilhoso em meio à natureza, deliciando-se com comidas típicas e se aventurando com as atividades radicais. Na sua experiência estão inclusas pensão completa e todas as estruturas do hotel e suas atividades como quadriciclo, passeios a cavalo e pedalinho. Diversão completa para a família.",
                        Included = new List<object>
                            {
                                "Day use no Hotel Fazenda Campo dos Sonhos para 4 pessoas;",
                                "Café da manhã, Almoço e café da tarde;",
                                new List<object>
                                    {
                                        "Toda área de lazer do hotel:",
                                        "Passeios a cavalo;",
                                        "Charretes;",
                                        "Pedalinhos;",
                                        "Quadrículos;",
                                        "Barcos;",
                                        "Bicicletas e Triciclos...;"
                                    },
                                new List<object>
                                    {
                                        "Toda estrutura do Hotel:",
                                        "Piscina aquecida coberta e descoberta;",
                                        "Sauna seca e úmida;",
                                        "Ducha Circular;",
                                        "Sala de ginástica;",
                                        "Salão de jogos;",
                                        "Campo de Futebol;",
                                        "Quadra poliesportiva;",
                                        "Banheira SPA para 8 pessoas."
                                    },
                                "Visita monitorada pela fazenda;",
                                "Brincadeiras com os monitores;",
                                "Torneios de pesca."
                            },
                        NotIncluded = new List<object>
                            {
                                "Uso de Chalé ou Apartamento;",
                                "Transporte até o local;",
                                "Bebidas durante a refeição;",
                                "Qualquer item não mencionado como incluso."
                            },
                        Important = new List<object>
                            {
                                "Agendamento com 1 semana de antecedência;",
                                "O Day Use inicia-se ás 07:30, e termina ás 18:00;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos."
                            },
                        Location = "Hotel Fazenda Campo dos Sonhos: Estrada dos Sonhos, KM 6 - Cx. Postal 02 - Lavras de Baixo - Socorro."
                    },

                    new RewardExperienceVM(1, 3, 3)
                    {
                        Name = "Noite de Musical (Para 2 Pessoas)",
                        Included = new List<object>
                            {
                                "2 entradas para um musical no Teatro Renault",
                                "Taxas de entrega."
                            },
                        NotIncluded = new List<object>
                            {
                                "Transporte até o local;",
                                "Estacionamento;",
                                "Qualquer item não mencionado como incluso."
                            },
                        Important = new List<object>
                            {
                                "O musical será no Teatro Renault e a peça será de acordo com a data de agendamento e está sujeito a disponibilidade.",
                                "Agendar com antecedência de pelo menos 1 semana;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos."
                            }
                    },

                    #endregion

                    /**
                     * PRODUCTS TIER 2
                     */
                    #region Products Tier 2
                    
                    new RewardItemVM(2, 1, 3)
                    {
                        Name = "Câmera GoPro HD",
                        Description = "Resolução 1080p, portátil, acoplável à roupa, prova d’agua 60m, captura vídeos de 1080p e 30 qps/ 720p e 60qps, opções de fotografia automática com velocidade de 10 fotos por segundo. 11 megapixels. Acessorios que acompanham a maquina: 01 camera, 01 caixa a prova D’agua, 01 bateria de lítio-ion recarregável, 01 cabo de carregamento USB, fivela QR, fivela J-Hook, pivô ajustável em 03 direções, 01 suporte curvo com adesivo, 01 suporte plano com adesivo."
                    },
                    new RewardItemVM(2, 2, 1)
                    {
                        Name = "Bicicleta 21 Marchas",
                        Description = "Bicicleta 21 Marchas Aro 26. Bicicleta de alta resistência e performance. Perfeita para atividade de lazer, como um simples passeio no parque até aventuras radicais em trilhas simples, independente do que vem pela frente: lama, tempestades, poças ou buracos. Continuaremos pedalando e superando limites."
                    },
                    new RewardItemVM(2, 3, 1)
                    {
                        Name = "Mini Ipad",
                        Description = "Mini Ipad retina preto e cinza com processador A5, display de 7.9”, câmera 5MP, 16GB de memória, iOS7 e Wi-Fi"
                    },
                    new RewardItemVM(2, 4, 2)
                    {
                        Name = "Voucher vale roupa nas lojas Brooksfield Donna ou Brooksfield Man - R$ 1.600,00"
                    },
                    new RewardItemVM(2, 5, 3)
                    {
                        Name = "Frigobar Retrô + Champagne Francês Veuve Clicquot com Fridge Rosé",
                        Description = "Frigobar Retrô 76L acompanhada de Champagne Francês Veuve Clicquot com Fridge Rosé"
                    },
                    new RewardItemVM(2, 6, 1)
                    {
                        Name = "Lava louça 8 serviços",
                        Description = "Com identificador de etapas com visor na porta. 08 serviços, 05 programas de lavagens, função acquaspray."
                    },
                    new RewardItemVM(2, 7, 1)
                    {
                        Name = "Smart TV Led 32”",
                        Description = "Smart TV Led 32” resolução HD, smart share, acesso a conteúdos Premium, time machine II, IPS e compatível com magic remote."
                    },
                    new RewardItemVM(2, 8, 2)
                    {
                        Name = "Playstation Portátil – Sony",
                        Description = "Possui saída A/V para televisores convencionais, possibilitando jogar na tela da TV. Reproduz música e vídeo, visualiza fotos, navega na internet, assiste TV e compartilha arquivos. Os jogos são em UMD (Universal Media Disc) suportando até 1,8 GB."
                    },

                    #endregion

                    /**
                     * EXPERIENCES TIER 2
                     */
                    #region Experiences Tier 2

                    new RewardExperienceVM(2, 1, 5)
                    {
                        Name = "Litoral com a Família no Itamambuca Ecoresort | Ubatuba (2 Dias e 1 Noite para 2 Adultos e 2 Crianças)",
                        Description = "A área verde do hotel é enorme e o destaque é para o esporte e o lazer. Monitores profissionais, ótima infraestrutura e equipamentos de alta qualidade. Na sua diária, incluso atividades como trilhas, jogos de piscina, hidroginástica, sinuca, dança de salão, caminhadas, yoga, tênis, basquete, vôlei de praia, musculação e muito mais. Diversão certa para a família.",
                        Included = new List<object>
                            {
                                "1 noite de hospedagem no Itamambuca Ecoresort em 01 Apto. Master para 2 adultos e 2 crianças de até 10 anos;",
                                "Café da Manhã."
                            },
                        NotIncluded = new List<object>
                            {
                                "Transporte até o local;",
                                "Bebidas e refeições não mencionadas;",
                                "Extras de caráter pessoal (telefonemas, bebidas, lavanderia, internet, compras, etc);",
                                "Qualquer item não mencionado acima."
                            },
                        Important = new List<object>
                            {
                                "Agendar com 15 dias de antecedência;",
                                "A experiência é válida somente para 2 crianças de até 10 anos de idade;",
                                "Sujeito a alterações e disponibilidade;",
                                "Experiência não é válida para o mês de janeiro;",
                                "Não é válido para Janeiro e feriados, datas comemorativas, eventos, grupos e mês de Janeiro."
                            }
                    },

                    new RewardExperienceVM(2, 2, 4)
                    {
                        Name = "Hotel Fazenda Dona Carolina com a Família | Itatiba (Para 2 Adultos e 2 Crianças)",
                        Description = "Aproveite um dia de muita diversão com a família no Hotel Fazenda Dona Carolina com um almoço A aérea verde do hotel é enorme e o destaque é para o esporte e o lazer. Monitores profissionais, ótima infraestrutura e equipamentos de alta qualidade. Incluso atividades como trilhas, jogos de piscina, hidroginástica, sinuca, dança de salão, caminhadas, yoga, tênis, basquete, vôlei de praia, musculação e muito mais.",
                        Included = new List<object>
                            {
                                "Day Use para 2 Adultos:",
                                "01 Refeição (sem bebidas);",
                                new List<object>
                                    {
                                        "Atividades de Lazer:",
                                        "Piscina climatizada;",
                                        "Sauna;",
                                        "Academia;",
                                        "Trilhas ecológicas;",
                                        "Tirolesa;",
                                        "Arco e flecha;",
                                        "Caiaque;",
                                        "Arvorismo;",
                                        "Cascading slackline",
                                        "Pesca esportiva;",
                                        "Tour da cachaça;",
                                        "Visita Histórica pela fazenda;",
                                        "Monitoria para crianças a partir de 03 anos."
                                    }
                            },
                        NotIncluded = new List<object>
                            {
                                "Bebidas ou qualquer consumo extra;",
                                "Transporte até o local;",
                                "Hospedagem no local;",
                                "Qualquer item não mencionado como incluso."
                            },
                        Important = new List<object>
                            {
                                "Necessário agendar com 15 dias de antecedência;",
                                "Duração do Day Use de 8 horas;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos."
                            }
                    },

                    new RewardExperienceVM(2, 3, 4)
                    {
                        Name = "Romance na Pousada Quinta da Serra | Cunha (3 Dias e 2 Noites para 2 Pessoas)",
                        Description = "Aconchego, romantismo e charme. A Pousada Quinta da Serra, a 5ª melhor Pousada do Brasil em 2013 pelo Tripadvisor, é um lugar mais do que especial para namorar e encantar os sentidos. Com um chalé que possui hidromassagem com vista panorâmica a 1.200 metros de altitude, a pousada vai inspirar os casais apaixonados.",
                        Included = new List<object>
                            {
                                "2 noites de hospedagem na Pousada Quinta da Serra em categoria Beija Flor ou Catavento;",
                                "Taxa de serviço;",
                                "1 Jantar com Foundue de Queijo e sobremesa."
                            },
                        NotIncluded = new List<object>
                            {
                                "Não inclui bebidas;",
                                "Qualquer item não mencionado como incluso."
                            },
                        Important = new List<object>
                            {
                                
                                "Agendamento com 15 dias de antecedência;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos."
                            }
                    },

                    #endregion

                    /**
                     * PRODUCTS TIER 3
                     */
                    #region Products Tier 3

                    new RewardItemVM(3, 1, 2)
                    {
                        Name = "Voucher vale roupa nas lojas Brooksfield Donna ou Brooksfield Man – R$ 3.200,00"
                    },
                    new RewardItemVM(3, 2, 1)
                    {
                        Name = "Notebook 14”",
                        Description = "Tela touch, Intel Core, i7-3520M, 8GB de memória, 1TB de HD, tela led touchscreen de 14”."
                    },
                    new RewardItemVM(3, 3, 1)
                    {
                        Name = "Smart TV Led 46”",
                        Description = "Smart TV Led 46” Dual Core função futebol, Full HD e Clear Motion Rate 120Hz."
                    },
                    new RewardItemVM(3, 4, 2)
                    {
                        Name = "Iphone 5S",
                        Description = "iPhone 5S Apple com Tela 4”, iOS 7, Touch ID, Câmera 8MP, Wi-Fi, 3G/4G, GPS, MP3 e Bluetooth."
                    },
                    new RewardItemVM(3, 5, 1)
                    {
                        Name = "Lava e Seca",
                        Description = "Lavadora e secadora de roupa 10,2Kg com 14 programas de lavagem, display touch, 6 motion, cor branca."
                    },
                    new RewardItemVM(3, 6, 2)
                    {
                        Name = "Home Theater Blu Ray 3D",
                        Description = "Home Theater com Blu-ray 3D Branco com 5.1 canais, HDMI, USB, Bluetooth, NFC, FULL HD, Wi-Fi, Wireless, Função Torcida e 850 W de Potência."
                    },
                    new RewardItemVM(3, 7, 2)
                    {
                        Name = "Câmera semi-profissional",
                        Description = "Câmera Digital 24.3 MP Tecnologia TMT e Lentes Intercambiáveis Lente SAL18-55mm."
                    },
                    new RewardItemVM(3, 8, 2)
                    {
                        Name = "Refrigerador 2 Portas",
                        Description = "Refrigerador 2 Portas Frost Free 423L com Painel Eletrônico, Freezer Inteligente, Tecnologia Inverter, Cor Branca."
                    },

                    #endregion

                    /**
                     * EXPERIENCES TIER 3
                     */
                    #region Experiences Tier 3

                    new RewardExperienceVM(3, 1, 5)
                    {
                        Name = "Stand Up Paddle no Litoral Norte | Juquehy Praia Hotel (3 Dias e 2 noites para 2 Pessoas)",
                        Description = "Experimente 1 hora de aula de Stand up Paddle em uma das praias mais lindas do litoral norte e se hospede no exclusivo Juquehy Praia Hotel.",
                        Included = new List<object>
                            {
                                "2 noites de hospedagem para 2 pessoas no Juquehy Praia Hotel;",
                                "Café da manhã;",
                                "Taxa de serviço;",
                                new List<object>
                                    {
                                        "01 hora de Aula de Stand up paddle para 2 pessoas:",
                                        "Instruções na areia antes de entrar no mar;",
                                        "1 hora de aula com instrutor."
                                    }
                            },
                        NotIncluded = new List<object>
                            {
                                "Transporte até o local;",
                                "Qualquer item não mencionado como incluso."
                            },
                        Important = new List<object>
                            {
                                "Agendamento com 15 dias de antecedência;",
                                "Não é válido para Janeiro;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos, grupos e mês de janeiro."
                            }
                    },

                    new RewardExperienceVM(3, 2, 4)
                    {
                        Name = "Relax na Pousada A Rosa e o Rei | São Francisco Xavier (3 Dias e 2 Noites para 2 Pessoas)",
                        Description = "Passe um final de semana em um ambiente de relaxamento e com vistas estonteantes. Localizada em plena Mata Atlântica, a Pousada Rosa e o Rei inclui na sua diária atividades como yôga e meditação. Na sua diária, incluso uma massagem relaxante. Tudo para você renovar suas energias!",
                        Included = new List<object>
                            {
                                "2 noites de hospedagem para 2 pessoas;",
                                "Café da manhã;",
                                "Taxa de serviço;",
                                "01 massagem relaxante de 50 minutos;",
                                new List<object>
                                    {
                                        "Atividades de lazer:",
                                        "Prática diária de Taichi-chuan;",
                                        "Prática de Yoga;",
                                        "Apreciação do fogo;",
                                        "Trilhas com cachoeiras (na próprioa Pousada);",
                                        "Sala de Ginástica;",
                                        "Filmes (DVD), internet, livros;",
                                        "Piscina natural;",
                                        "Cabana do silêncio."
                                    }
                            },
                        NotIncluded = new List<object>
                            {
                                "Refeições não mencionadas acima;",
                                "Transporte até o hotel;",
                                "Massagens terapêuticas, reflexologia, pedilúvio, banhos, trilhas fora da pousada, cavalgadas;",
                                "Qualquer item não mencionado como incluso."
                            },
                        Important = new List<object>
                            {
                                "Agendamento com 15 dias de antecedência;",
                                "A experiência é válida somente para os finais de semana.",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos."
                            }
                    },

                    new RewardExperienceVM(3, 3, 3)
                    {
                        Name = "Jantar No Escuro na sua Casa | Experiência Sensorial no Escuro (Para 2 Pessoas)",
                        Description = "No Escuro Gastronomia: Promove jantares em que o menu é elaborado para ser degustado de olhos vendados. Um momento especial, onde o alimento toma uma dimensão diferente, descubra sua textura, o aroma e pequenos detalhes que não percebemos normalmente, mergulhe no rico universo da sensoriedade. O ambiente é cuidadosamente arranjando para aconchegar e sensibilizar cada um. Músicas e outras performances são utilizadas para entreter e aguçar mais os sentidos.",
                        Included = new List<object>
                            {
                                "A experiência dura uma média de 3 horas;",
                                "Menu degustação harmonizado com vinhos e outras bebidas. O menu é sempre surpresa e único;",
                                "Concepção, acompanhamento e coordenação do evento;",
                                "Equipe de salão;",
                                "Músicos;",
                                "Intervenções sensoriais diversas."
                            },
                        NotIncluded = new List<object>
                            {
                                "Qualquer item não mencionado como inlcuso."
                            },
                        Important = new List<object>
                            {
                                "Agendamento com 15 dias de antecedência;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos."
                            }
                    },

                    new RewardExperienceVM(3, 4, 2)
                    {
                        Name = "Porsche + Ferrari Experience (Para 1 Pessoa)",
                        Description = "A experiência perfeita para os amantes dos automóveis de luxo. Acelere na melhor rodovia do Brasil e sinta toda a emoção de dirigir uma Porsche e uma Ferrari.",
                        Included = new List<object>
                            {
                                new List<object>
                                    {
                                        
                                        "Driving Experience para 1 pessoa: O passeio é realizado na Rodovia dos Bandeirantes e acompanhado por um guia da Drive4Fun no banco do passageiro, para orientar e explicar todos os recursos disponíveis no automóvel:",
                                        "Porsche por 30 km;",
                                        "Ferrari por 30 km."
                                    }
                            },
                        NotIncluded = new List<object>
                            {
                                "Transporte até o local;",
                                "Estacionamento;",
                                "Qualquer item não mencionado como inlcuso."
                            },
                        Important = new List<object>
                            {
                                "A experiência tem duração de aproximadamente 40 minutos;",
                                "Necessário carteira de habilitação em território nacional (tem que ser habilitado mínimo já de 2 anos).",
                                "Os passeios são realizados na Rodovia dos Bandeirantes, sendo a distância total percorrida de 30 KM por carro.",
                                "A experiência é realizada sempre acompanhado de um responsável da equipe;",
                                "A idade mínima para dirigir é de 25 anos.",
                                "Agendar com pelo menos 15 dias de antecedência;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos"
                            },
                        Location = "Ponto de encontro: Hotel Quality Itupeva: Fazenda Serra Azul – Gleba B, S/Nº (Rodovia dos bandeirantes, km72 – Sentido São Paulo) - Itupeva – São Paulo."
                    },

                    #endregion

                    /**
                     * PRODUCTS TIER 4
                     */
                    #region Products Tier 4

                    new RewardItemVM(4, 1, 3)
                    {
                        Name = "Kit X-Box One + TV de LED 42”",
                        Description = "Console Xbox One com kinect preto com 8GB de memória RAM e 500GB de HD + Smart TV de Led 42” com smart share, time machine II e compatível com magic remote."
                    },
                    new RewardItemVM(4, 2, 3)
                    {
                        Name = "Playstation 4 + 03 jogos",
                        Description = "O PlayStation®4 é o melhor lugar para jogar jogos dinâmicos e conectados, com gráfico rico e alta velocidade, personalização inteligente, funcionalidades sociais altamente integradas e recursos de segunda tela inovadores. Combinando conteúdo sem igual, experiências de jogo envolventes, todos os seus aplicativos favoritos de entretenimento digital e as exclusividades do PlayStation, o PS4 é centrado nos jogadores, permitindo-lhes que joguem quando, onde e como quiserem. O PS4 permite que os melhores desenvolvedores de jogos do mundo liberem sua criatividade e estendam os limites do jogo por meio de um sistema que está sintonizado especificamente com suas necessidades + 01 jogo Call of Duty + 01 jogo NBA 2K14 + 01 jogo Assassin’s Creed IV."
                    },
                    new RewardItemVM(4, 3, 1)
                    {
                        Name = "Vale Jóia da Vivara",
                        Description = "Vale compra no valor de R$ 4.800,00 da joalheria Vivara."
                    },
                    new RewardItemVM(4, 4, 2)
                    {
                        Name = "MacBook Air",
                        Description = "Macbook Air Apple, Intel Core i5 1,3 GHz, 4GB de memória, 256 GB de HD SSD, tela LED 11,6” e OS X Mountain Lion."
                    },
                    new RewardItemVM(4, 5, 4)
                    {
                        Name = "Kit Linha Branca - Kit de refrigerador, Máquina de lavar roupas e Fogão.",
                        Description = "Refrigerador 2 Portas 352L Frost Free / Display Eletrônico em Led Laranja / Branco. Lavadora de Roupa 11Kg Automática com 5 Programas de Lavagem, Função Turbo Performance, Cor Branca. Fogão de Piso 4 Bocas com Acendimento Automático / Timer Digital Sonoro / Quadrichama / Prateleira Deslizante / Branco"
                    },
                    new RewardItemVM(4, 6, 2)
                    {
                        Name = "Voucher de compras nas lojas Tok&Stok ou Etna",
                        Description = "O ganhador receberá um voucher no valor de R$ 4.800,00 para trocar em produtos nas lojas Tok&Stok ou Etna."
                    },
                    new RewardItemVM(4, 7, 3)
                    {
                        Name = "Samsung Galaxy S4 + Tablet Galaxy Note 8.0",
                        Description = "01 Smartphone Samsung Galaxy S4 Desbloqueado Tim Preto com Android 4.2, 4G, Tela Super AMOLED de 5” Full HD, Câmera de 13 MP e Processador Snapdragon de 1.9 GHz  + 01 Tablet Galaxy Note 8.0 N5110 Branco com Tela 8”, Wi-Fi, Android, Bluetooth, Processador 1.6GHz Quad Core, 2 GB de Memória RAM e 16 GB de Memória Interna"
                    },

                    #endregion

                    /**
                     * EXPERIENCES TIER 4
                     */
                    #region Experiences Tier 4

                    new RewardExperienceVM(4, 1, 4)
                    {
                        Name = "Lancha Privativa em Ilhabela | Pousada Refúgio das Pedras (3 Dias e 2 Noites para 2 Pessoas)",
                        Description = "Que tal um dia inteiro de lancha privativa pelas belíssimas praias de Ilhabela? Tenha essa experiência incrível e ainda se hospede na Pousada Refúgio das Pedras que faz parte da Associação Roteiros de Charme.",
                        Included = new List<object>
                            {
                                "2 noites de hospedagem para 2 pessoas em Suite Standard casal;",
                                "Café da manhã;",
                                "Taxa de serviço;",
                                new List<object>
                                    {
                                        "Passeio de lancha das 09hs as 18hs",
                                        "Roteiro: Praia da Fome, Praia do Eustáquio e Praia de Castelhanos."
                                    },
                                "Bebidas (água, cerveja, sucos) a bordo."
                            },
                        NotIncluded = new List<object>
                            {
                                "Refeições não mencionadas acima;",
                                "Transporte até o hotel;",
                                "Qualquer item não mencionado como incluso."
                            },
                        Important = new List<object>
                            {
                                "Agendamento com 15 dias de antecedência;",
                                "Sujeito a alterações e disponibilidade;",
                                "Reservas somente de sexta a domingo;",
                                "Não é válido para Janeiro, Fevereiro, Março, feriados, datas comemorativas, eventos e grupos."
                            }
                    },

                    new RewardExperienceVM(4, 2, 2)
                    {
                        Name = "Tour Gastronômico em São Paulo (Para 2 Pessoas)",
                        Description = "Desfrute de um tour gastronômico de dar água na boca em 4 dos melhores restaurante de São Paulo: Skye, Tre Bicchieri, Varanda Grill e A Figueira Rubaiyat.",
                        Included = new List<object>
                            {
                                "Menu degustação com vinho no Restaurante Figueira Rubayat para 2 pessoas;",
                                new List<object>
                                    {                                        
                                        "Menu degustação Skye Super Especial com vinho no Restaurante Skye para 2 pessoas;",
                                        "Taxa de serviço;"
                                    },
                                new List<object>
                                    {                                        
                                        "Menu degustação no Restaurante Tre Bicchieri para 2 pessoas (incluso estacionamento);",
                                        "Taxa de serviço;"
                                    },
                                new List<object>
                                    {                                        
                                        "Menu degustação no Restaurante Varanda Jardins para 2 pessoas.",
                                        "Taxa de serviço;"
                                    }
                            },
                        NotIncluded = new List<object>
                            {
                                "Transporte até o local;",
                                "Estacionamento (com exceção do Restaurante Tre Bichieri);",
                                "Qualquer item não mencionado como incluso."
                            },
                        Important = new List<object>
                            {
                                "Agendamento com 15 dias de antecedência;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos."
                            }
                    },

                    new RewardExperienceVM(4, 3, 2)
                    {
                        Name = "Hotel Fazenda Mazzaropi (4 Dias e 3 Noites para 2 Adultos e 1 Criança)",
                        Description = "Curta um dos melhores hotéis fazenda para crianças do Brasil. O Fazenda Mazzaropi tem diversas atividades e área de lazer completa, perfeito para levar a família.",
                        Included = new List<object>
                            {
                                "3 noites de hospedagem no Hotel Fazenda Mazzaropi em Apto Standard para 2 Adultos e 1 Criança de até 10 anos;",
                                "Pensão Completa (café da manhã, almoço, jantar)",
                                "Taxas de serviço;",
                                new List<object>
                                    {
                                        "Atividades de lazer:",
                                        "Festa Caipira;",
                                        "Passeio ao Sítio do Pica-Pau Amarelo;",
                                        "Atividades recreativas com acompanhamento de monitores no período de 9h00 às 22h00;",
                                        "Passeio a cavalo;",
                                        "Visita ao Museu Mazzaropi;",
                                        "Utilização da área de lazer como as quadras de tênis, quadra poliesportiva, pista de bocha, sauna, academia, campo oficial de futebol,pscinas (01 coberta aquecida, 01 com toboágua e 01 infantil climatizada),quadra de volei de areia,cantinho da mamãe, salão de jogos entre outros."
                                    }
                            },
                        NotIncluded = new List<object>
                            {
                                "Transporte até o local;",
                                "Bebidas;",
                                "Atividades pagas a parte (pedalinho, arvorimos, aluguel bicicleta, etc)",
                                "Qualquer item não mencionado como incluído."
                            },
                        Important = new List<object>
                            {
                                "Agendamento com 15 dias de antecedência;",
                                "Não é válido para Janeiro;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos, não é válido para o mês de Janeiro."
                            },
                        Location = "Estrada Municipal dos Remedio, 2380 | Taubaté - SP"
                    },

                    new RewardExperienceVM(4, 4, 5)
                    {
                        Name = "SPA e Resort 7 Voltas (4 Dias e 3 Noites para 2 Pessoas)",
                        Description = "Premiado e reconhecido por seu padrão único de excelência, o Sete Voltas Spa Resort é sinônimo de bem-estar, qualidade de vida e longevidade e é referência ainda em hospedagem e acompanhamentos personalizados. Sua hospedagem inclui avaliação com nutricionista, refeições balanceadas e diversas atividades físicas.",
                        Included = new List<object>
                            {
                                "3 noites de hospedagem em Apto. Duplo Bromélias;",
                                "Avaliação com a nutricionista;",
                                "5 refeições diárias;",
                                "Atividades físicas de 1 em 1 hora."
                            },
                        NotIncluded = new List<object>
                            {
                                "Refeições não mencionadas acima;",
                                "Transporte até o hotel;",
                                "Qualquer item não mencionado como incluso."
                            },
                        Important = new List<object>
                            {
                                
                                "Agendamento com 15 dias de antecedência;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos."
                            }
                    },

                    #endregion

                    /**
                     * PRODUCTS TIER 5
                     */
                    #region Products Tier 5

                    new RewardItemVM(5, 1, 2)
                    {
                        Name = "Voucher de compras nas lojas Tok&Stok ou Etna",
                        Description = "O ganhador receberá um voucher no valor de R$ 8.000,00 para trocar em produtos nas lojas Tok&Stok ou Etna."
                    },
                    new RewardItemVM(5, 2, 1)
                    {
                        Name = "Macbook Pro Apple",
                        Description = "MacBook Apple Pro Retina com Processador Intel® Core™ i5 Dual Core 2.6GHz, 8GB de Memória, HD 512GB Tela LED 13,3” e OS X Mavericks."
                    },
                    new RewardItemVM(5, 3, 1)
                    {
                        Name = "Smart TV 3D LED 55”",
                        Description = "Smart TV 3D LED 55” Full HD com Função Futebol, Smart Interaction, Wi-Fi, Processador Quad Core e Clear Motion Rate 960Hz"
                    },
                    new RewardItemVM(5, 4, 1)
                    {
                        Name = "Honda Biz 125 Flex",
                        Description = "Moto Honda Biz 125 Flex. Agrada aos mais variados estilos e idades, além de atender às necessidades do público que procura independência na locomoção e prazer ao pilotar. O modelo líder da categoria Family.  Cores disponíveis: Preto fosco e branco."
                    },new RewardItemVM(5, 5, 4)
                    {
                        Name = "Kit Apple - Mini Ipad + Macbook Air + Iphone 5C",
                        Description = "Mini Ipad Apple Branco com Tela 7.9”, 3G, Wi-Fi, Câmera iSight 5 MP, Câmera Frontal 1,2MP e Vídeos em HD + 01 MacBook Air Apple com 4ª Geração do Processador Intel® Core™ i5 1,3 GHz, 4 GB de Memória, 128 GB de HD SSD, Tela LED 11,6” e OS X Mountain Lion + 01 iPhone 5C Branco, Processador A6, iOS 7, Câmera de 08 MP, Display de 4”, 4G e Wi-Fi."
                    },
                    new RewardItemVM(5, 6, 4)
                    {
                        Name = "Kit Linha Branca - Lava e Seca + Refrigerador Frost Free Duplex + Purificador de Água",
                        Description = "Lava e Seca 10kg e 6kg e 12 Programas de Lavagem, Refrigerador Frost Free Duplex - 403L c/ Smart Ice, Purificador de Água com Painel Blue Touch"
                    },

                    #endregion

                    /**
                     * EXPERIENCES TIER 5
                     */
                    #region Experiences Tier 5

                    new RewardExperienceVM(5, 1, 3)
                    {
                        Name = "Snowland Gramado (4 Dias e 3 Noites para 2 Pessoas)",
                        Description = "Viva a Neve! Seja inverno ou verão, divirta-se no Snowland, o primeiro parque de neve indoor das Américas voltado para a prática de esportes de neve. Transporte a sua imaginação para um charmoso vilarejo alpino ao sopé de uma montanha de neve. Localizado em Gramado, na Serra Gaúcha. O parque oferece diversas atividades na neve para todas as idades e gosto, incluindo várias atrações na neve como o esqui, snowboarding e airboard.",
                        Included = new List<object>
                            {
                                "Passagem aérea de ida e volta São Paulo/ Porto Alegre/ São Paulo;",
                                "Taxas de embarque;",
                                "3 noites de hospedagem na Estalagem St. Hubert ou similar;",
                                "Café da manhã;",
                                "Taxa de serviço;",
                                "Locação de carro;",
                                new List<object>
                                    {
                                        "2 entradas para o Snowland ou Esqui:",
                                        "Acesso ao vilarejo alpino para todo o dia;",
                                        "01 aluguel de roupa de neve (jaqueta, calça, bota, capacete de segurança);",
                                        "Luva com a marca Snowland (suvenir, você pode levar para casa);",
                                        "01 Acesso a Montanha de Neve com tempo livre;"
                                    },
                                "Aula de snowland ou esqui por 1 hora."
                            },
                        NotIncluded = new List<object>
                            {
                                "Transporte até o local;",
                                "Qualquer item não mencionado acima."
                            },
                        Important = new List<object>
                            {
                                "Documentações Necessárias: Carteira de Identidade (RG) original, emitida pela Secretaria de Segurança Pública com máximo 10 anos de emissão, em bom estado e com foto.",
                                "Agendar com pelo menos 2 meses de antecedência;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos."
                            }
                    },

                    new RewardExperienceVM(5, 2, 5)
                    {
                        Name = "Voo de Helicoptero nas Cataratas | Hotel Orient Express (3 Dias e 2 Noiteas para 2 Pessoas)",
                        Description = "Sobrevoe uma das paisagens mais incríveis do mundo, as Cataratas do Iguaçu. Sua hospedagem será no Hotel das Cataratas, membro da luxuosa Rede orient Express e o único situado dentro do Parque Nacional Iguaçú. O hotel proporciona uma das mais ricas experiências para o visitante: a exuberante vista das quedas d´água.",
                        Included = new List<object>
                            {
                                "Passagem aérea de ida e volta;",
                                "Taxas de embarque;",
                                "Transfer Aeroporto / Hotel / Aeroporto - privativo;",
                                "Transfer in/out até o helicóptero - regular;",
                                "Passeio de Helicoptero de até 10 min;",
                                "2 noites de hospedagem para 2 pessoas em Apto. Luxo com vista Jardim ou Floresta;",
                                "Café da manhã;",
                                "Taxas de serviço;"
                            },
                        NotIncluded = new List<object>
                            {
                                new List<object>
                                    {
                                        "Taxa de entrada do Parque Nacional deve ser paga no local por todos os visitantes, incluindo hóspedes do Hotel das Cataratas:",
                                        "Brasileiros: R$ 28,80",
                                        "Mercosul: R$ 38,80",
                                        "Outros países: R$ 48,80"
                                    },
                                "Qualquer item não mencionado acima."
                            },
                        Important = new List<object>
                            {
                                "Documentações Necessárias: Carteira de Identidade (RG) original, emitida pela Secretaria de Segurança Pública com máximo 10 anos de emissão, em bom estado e com foto.",
                                "Agendar com pelo menos 2 meses de antecedência;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para feriados, datas comemorativas, eventos e grupos."
                            }
                    },

                    new RewardExperienceVM(5, 3, 7)
                    {
                        Name = "Arraial D'Ajuda Ecoresort (5 Dias e 4 Noites para 2 Adultos)",
                        Description = "Divirta-se no resort mais charmoso da Bahia. O Arraial D'Ajuda Eco Resort exibe sofisticação, elegância e bom gosto em cada um dos seus ambientes. Planejado para oferecer o máximo de conforto, atende às expectativas mais elevadas: gastronomia premiada, variadas atividades de lazer e atendimento diferenciado. Sua estadia inclui café e jantar.",
                        Included = new List<object>
                            {
                                "Passagem aérea de ida e volta em classe econômica;",
                                "Taxas de embarque;",
                                "Transfer Aeroporto/ Hotel/ Aeroporto;",
                                "4 noites de hospedagem no Arraial D’Ajuda Eco Resort em 01 Apto. Standard;",
                                "Meia Pensão – Café da manhã e Jantar (bebidas não inclusas);",
                                "Taxas de serviço."
                            },
                        NotIncluded = new List<object>
                            {
                                "Qualquer item não mencionado como incluso;"
                            },
                        Important = new List<object>
                            {
                                "Documentações Necessárias: Carteira de Identidade (RG) original, emitida pela Secretaria de Segurança Pública com máximo 10 anos de emissão, em bom estado e com foto.",
                                "Agendar com 2 meses de antecedência;",
                                "Sujeito a alterações e disponibilidade;",
                                "Não é válido para o mês de janeiro;",
                                "Não é válido para feriados, datas comemorativas, eventos, grupos e janeiro."
                            }
                    }

                    #endregion
            };
        }
    }

    public enum RewardCategory
    {
        PRODUCT = 1,
        EXPERIENCE = 2,
        GIFT = 3
    }
    
    public class RewardItemVM
    {

        public RewardItemVM(int tier, int number, int numberOfImages, RewardCategory category = RewardCategory.PRODUCT)
        {
            Tier = tier;
            Category = category;
            Id = Convert.ToInt32(category.ToString("D") + tier.ToString("D4") + number.ToString("D4"));

            var Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var imageName = category == RewardCategory.PRODUCT ? "premio" : "experiencia";
            ThumbnailUrl = Url.Content("~/Content/rewards/" + imageName + "-" + tier + "-" + number + "-" + "1.png");

            ImagesUrls = new List<string>();
            for (var i = 1; i <= numberOfImages; i++)
            {
                ImagesUrls.Add(Url.Content("~/Content/rewards/" + imageName + "-" + tier + "-" + number + "-" + i + ".png"));
            }

            RewardUrl = Url.Action("Rewards", "Member", new { rewardId = Id, rewardTier = Tier});
        }

        public int Id { get; set; }
        public string ThumbnailUrl { get; set; }
        public List<string> ImagesUrls { get; set; }
        public string RewardUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RewardCategory Category { get; set; }
        public int Tier { get; set; }
    }

    public class RewardExperienceVM : RewardItemVM
    {
        public RewardExperienceVM(int tier, int number, int numberOfImages) 
            : base(tier, number, numberOfImages, RewardCategory.EXPERIENCE)
        {
        }

        public List<object> Included { get; set; }
        public List<object> NotIncluded { get; set; }
        public List<object> Important { get; set; }
        public string Location { get; set; }
    }
}