import React, {useEffect, useState} from "react";
import PageWrapper from "../components/common/PageWrapper";
import {useParams} from "react-router-dom";
import type {ActorResponse} from "../models/actor.ts";
import {getActorById} from "../services/actors/getActorById.ts";
import ActorPortrait from "../components/actors/ActorPortrait.tsx";
import ActorTitle from "../components/actors/ActorTitle.tsx";
import ActorInfo from "../components/actors/ActorInfo.tsx";
import PhotoCarousel from "../components/common/PhotosCarousel.tsx";
import ActorMoviesCarousel from "../components/actors/ActorMoviesCarousel.tsx";
import Header from "../components/common/Header.tsx";
import Divider from "../components/common/Divider.tsx";

const ActorPage: React.FC = () => {
  const { actorId } = useParams();
  const [actor, setActor] = useState<ActorResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!actorId) {
      setError("Empty ID in URL");
      setLoading(false);
      return;
    }

    const fetchData = async () => {
      try {
        const data = await getActorById(actorId);
        setActor(data);
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
        <div className="flex flex-col items-center">
          <div className="w-full flex flex-col lg:flex-row gap-10 justify-center">
            <ActorPortrait photoUrl={actor?.photoUrl} firstName={actor?.firstName} lastName={actor?.lastName} />

            <div className="flex-1 max-w-3xl space-y-8">
              {actor && <ActorTitle firstName={actor?.firstName} lastName={actor?.lastName} />}
              {actor && <ActorInfo biography={actor?.biography} birthDate={actor?.birthDate} />}
            </div>
          </div>

          <div className="w-full mt-5 max-w-5xl">
            {(actor?.movies ?? []).length > 0 && (
              <>
                <Divider/>
                <div className="mt-10 mb-10">
                  <ActorMoviesCarousel movies={actor?.movies ?? []}/>
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