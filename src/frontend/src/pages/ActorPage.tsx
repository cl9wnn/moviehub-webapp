import React, {useEffect, useState} from "react";
import PageWrapper from "../components/common/PageWrapper";
import {useParams} from "react-router-dom";
import type {ActorData} from "../models/actor.ts";
import {getActorById} from "../services/actors/getActorById.ts";
import ActorPortrait from "../components/actors/ActorPortrait.tsx";
import ActorTitle from "../components/actors/ActorTitle.tsx";
import ActorInfo from "../components/actors/ActorInfo.tsx";
import PhotoCarousel from "../components/common/PhotosCarousel.tsx";
import ActorMoviesCarousel from "../components/actors/ActorMoviesCarousel.tsx";
import Header from "../components/header/Header.tsx";
import Divider from "../components/common/Divider.tsx";
import {addFavoriteActor, removeFavoriteActor} from "../services/users/toggleFavoriteActor.ts";

const ActorPage: React.FC = () => {
  const { actorId } = useParams();
  const [actor, setActor] = useState<ActorData | null>(null);
  const [isFavorite, setIsFavorite] = useState(false);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const toggleFavorite = async () => {
    if (!actorId) return;

    try {
      if (isFavorite) {
        await removeFavoriteActor(actorId);
        setIsFavorite(false);
      } else {
        await addFavoriteActor(actorId);
        setIsFavorite(true);
      }
    } catch (err) {
      if (err instanceof Error) {
        setError(err.message);
      } else {
        setError("Не удалось изменить статус избранного");
      }
    }
  };

  useEffect(() => {
    if (!actorId) {
      setError("Empty ID in URL");
      setLoading(false);
      return;
    }

    const fetchData = async () => {
      try {
        const actorResponse = await getActorById(actorId);
        setActor(actorResponse.actor);
        setIsFavorite(actorResponse.isFavorite);
      } catch (err) {
        if (err instanceof Error) {
          setError(err.message);
        } else {
          setError("Something went wrong. Please try again.");
        }
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [actorId]);

  if (loading) return <div className="text-center mt-10 text-lg">Загрузка...</div>;
  if (error) return <div className="text-center mt-10 text-red-600">{error}</div>;

  return (
    <>
      <Header/>
      <PageWrapper>
        <div className="flex flex-col items-center mt-6">
          <div className="w-full flex flex-col lg:flex-row gap-10 justify-center">
            <ActorPortrait photoUrl={actor?.photoUrl} firstName={actor?.firstName} lastName={actor?.lastName} />

            <div className="flex-1 max-w-3xl space-y-8">
              {actor && <ActorTitle firstName={actor?.firstName} lastName={actor?.lastName} isFavorite={isFavorite} onToggleFavorite={toggleFavorite}/>}
              {actor && <ActorInfo biography={actor?.biography} birthDate={actor?.birthDate} />}
            </div>
          </div>

          <div className="w-full mt-5 max-w-5xl">
            {(actor?.movies ?? []).length > 0 && (
              <>
                <Divider/>
                <div className="mt-10 mb-10">
                  <ActorMoviesCarousel movies={actor?.movies ?? []} title="Фильмография"/>
                </div>
              </>
            )}

            {(actor?.photos ?? []).length > 0 && (
              <>
                <Divider/>
                <div className="mt-10 mb-10">
                  <PhotoCarousel photos={actor?.photos ?? []}/>
                </div>
              </>
            )}
          </div>
        </div>
      </PageWrapper>
    </>
  );
}

export default ActorPage;